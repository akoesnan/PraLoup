using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Entities;
using System.Web.Security;
using PraLoup.DataAccess.Interfaces;
using PraLoup.Facebook;
using PraLoup.Plugins;
using PraLoup.FacebookObjects;

namespace PraLoup.BusinessLogic
{
    public partial class AccountBase : MembershipUser
    {
        protected Account account = null;

        protected string[] _friends = null;

        protected static object mutex = new object();

        internal IRepository Repository { get; set; }

        private EventLogic eventLogic { get; set; }
        private ActivityLogic activityLogic { get; set; }

        public FacebookAccount FacebookAccount { get; set; }
        public EventActions EventActions { get; set; }

        public AccountBase(IRepository gr, IEnumerable<IEventAction> eventActionPlugins)
        {
            this.Repository = gr;
            this.eventLogic = new EventLogic(this.Repository);
            this.activityLogic = new ActivityLogic(this.Repository);
            this.EventActions = new EventActions(this.account, this.Repository, eventActionPlugins);
            this.SetupFacebookAccount();
        }

        public string[] Friends
        {
            get
            {
                if (_friends == null)
                {
                    lock (mutex)
                    {
                        if (_friends == null)
                        {
                            if (String.IsNullOrEmpty(account.Friends))
                            {
                                _friends = new string[] { };
                            }
                            else
                            {
                                _friends = account.Friends.Split(new char[] { ';' });
                            }
                        }
                    }
                }
                return _friends;
            }
        }

        /// <summary>
        /// Fetch from store
        /// </summary>
        /// <returns>true if fetch was successful</returns>
        public Account Fetch(string UserId)
        {
            return this.Repository.FirstOrDefault<Account>(a => a.UserId == UserId);
        }

        private Account FindById(int id)
        {
            return this.Repository.Find<Account>(id);
        }

        private Account FindByUserName(string userName)
        {
            return this.Repository.FirstOrDefault<Account>(a => String.Equals(userName, a.UserName, StringComparison.InvariantCultureIgnoreCase));
        }

        public void SetupFacebookAccount() {
            this.FacebookAccount = new FacebookAccount(this.account);
            var acct = this.Fetch(this.FacebookAccount.Account.UserId);
            if (acct == null)
            {
                this.Repository.Add<Account>(this.FacebookAccount.Account);
            }
            this.account = this.FacebookAccount.Account;
            this.Repository.SaveChanges();           
        }

        //public bool IsCreated()
        //{
        //    return Fetch(account) != null;
        //}

        protected void Register(bool create)
        {
            this.Repository.Add<Account>(account);
            this.Repository.SaveChanges();            
        }

        public Account GetAccount()
        {
            return this.account;
        }

        public Permissions GetPermissions(Event e)
        {
            Permissions mask = Permissions.EmptyMask;
            bool isOwner = false;
            bool isFriend = false;
            bool isFriendOfFriend = false;

            if (e.Organizers != null)
            {
                isOwner = e.Organizers.Any(x => x.Id == this.account.Id);
                foreach (var x in e.Organizers)
                {
                    isFriend |= this.IsFriend(x);
                    isFriendOfFriend |= this.IsFriendOfFriend(x);
                }
            }

            if (isOwner)
            {
                mask |= Permissions.Copy;
                mask |= Permissions.Delete;
                mask |= Permissions.Edit;
                mask |= Permissions.Modify;
            }


            switch (e.Privacy)
            {
                // public event can be viewed, shared and accpeted by everyone 
                case DataAccess.Enums.Privacy.Public:
                    mask |= Permissions.View;
                    mask |= Permissions.Share;
                    mask |= Permissions.Accept;
                    break;
                // friend only event can be viewed and accpeted by friends and owner only
                case DataAccess.Enums.Privacy.Friends:
                    if (e.Organizers != null)
                    {
                        foreach (var x in e.Organizers)
                        {
                            isFriend |= this.IsFriend(x);
                        }
                    }
                    if (isFriend || isOwner)
                    {
                        mask |= Permissions.View;
                        mask |= Permissions.Accept;
                    }
                    break;
                // friend of friend can view and accept 
                case DataAccess.Enums.Privacy.FriendsOfFriend:
                    if (e.Organizers != null)
                    {
                        foreach (var x in e.Organizers)
                        {
                            isFriend |= this.IsFriend(x);
                            isFriendOfFriend |= this.IsFriendOfFriend(x);
                        }
                    }
                    if (isFriendOfFriend || isFriend || isOwner)
                    {
                        mask |= Permissions.View;
                        mask |= Permissions.Accept;
                    }
                    break;
                // private means that only the owner can view and and accept
                // Todo: how about people that are invited explicitly?
                case DataAccess.Enums.Privacy.Private:
                    if (isOwner)
                    {
                        mask |= Permissions.View;
                        mask |= Permissions.Accept;
                    }
                    break;
            }
            mask |= Permissions.View;
            return mask;
        }

        public bool IsFriend(Account ab)
        {
            return Friends.Any(a => a == ab.UserId);
        }

        public bool IsFriendOfFriend(Account ab)
        {
            if (String.IsNullOrEmpty(ab.Friends))
            {
                return false;
            }
            string[] friends = ab.Friends.Split(new char[] { ';' });
            var result = from a in this.Friends
                         where friends.Any(c => c == a)
                         select a;

            return (result.Count() > 0);
        }

        public bool IsInvited(ICollection<Invitation> e)
        {
            
            var result = from a in e
                         where a.Recipients != null && a.Recipients.Any(b => b.UserId == this.account.UserId)
                         select a;

            return (result.Count() > 0);
        }

        public Permissions GetPermissions(Activity e)
        {
            Permissions mask = Permissions.EmptyMask;
            bool isOwner = false;
            bool isFriend = false;
            bool isFriendOfFriend = false;
            bool isInvited = false;

            if (activity.Organizer != null)
            {
                isOwner = activity.Organizer.Id == this.account.Id;
                if (!isOwner)
                {
                    isFriend = IsFriend(activity.Organizer);
                    isFriendOfFriend = IsFriendOfFriend(activity.Organizer);
                    isInvited = this.EventActions.IsInvited(activity);
                }
            }

            if (isOwner)
            {
                mask |= Permissions.Copy;
                mask |= Permissions.Delete;
                mask |= Permissions.Edit;
                mask |= Permissions.Modify;
                mask |= Permissions.InviteGuests;
            }

            if (isInvited)
            {
                mask |= Permissions.Accept;
                mask |= Permissions.View;
                mask |= Permissions.Copy;
            }

            // for activities, privacy means who can invite people to the activity.
            switch (activity.Privacy)
            {
                case DataAccess.Enums.Privacy.Public:
                    mask |= Permissions.View;
                    mask |= Permissions.Share;
                    mask |= Permissions.Accept;
                    mask |= Permissions.InviteGuests;
                    break;
                case DataAccess.Enums.Privacy.Friends:
                    if (isFriend && isInvited)
                    {
                        mask |= Permissions.InviteGuests;
                    }

                    break;
                case DataAccess.Enums.Privacy.FriendsOfFriend:
                    if ((isFriendOfFriend || isFriend) && isInvited)
                    {
                        mask |= Permissions.InviteGuests;
                    }
                    break;
                case DataAccess.Enums.Privacy.Private:
                    break;
            }
            if (mask != Permissions.EmptyMask)
            {
                mask |= Permissions.View;
            }
            return mask;
        }

        public Permissions GetPermissions(InvitationResponse e)
        {
            Permissions mask = Permissions.EmptyMask;
            bool isOwner = false;
            bool isActivtyOwner = false;

            if (isOwner || isActivtyOwner)
            {
                mask |= Permissions.Delete;
            }

            return mask;
        }

        public Permissions GetPermissions(Invitation e)
        {
            Permissions mask = Permissions.EmptyMask;
            bool isOwner = e.Recipient == this.account;
            bool isActivtyOwner = e.Activity.Organizer == this.account;

            if (isOwner || isActivtyOwner)
            {
                mask |= Permissions.Delete;
            }
            return Permissions.EmptyMask;
        }
    }
}
