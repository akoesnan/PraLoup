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
        }

        public string[] Friends
        {
            get
            {
                if (Friends == null)
                {
                    lock (mutex)
                    {
                        if (Friends == null)
                        {
                            _friends = account.Friends.Split(new char[] { ';' });
                        }
                    }
                }
                return _friends;
            }
        }

        public Account Find(Account account)
        {
            // find by account id
            if (account.Id != 0)
            {
                return FindById(account.Id);
            }
            // 
            else if (!string.IsNullOrEmpty(account.UserName))
            {
                return FindByUserName(account.UserName);
            }
            return null;
        }

        private Account FindById(int id)
        {
            return this.Repository.Find<Account>(id);
        }

        private Account FindByUserName(string userName)
        {
            return this.Repository.FirstOrDefault<Account>(a => String.Equals(userName, a.UserName, StringComparison.InvariantCultureIgnoreCase));
        }


        public bool IsCreated()
        {
            return Find(account) != null;
        }

        public void Register()
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

            bool isOwner = e.Organizers.Any(x => x.Id == this.account.Id);

            if (isOwner)
            {
                mask |= Permissions.Copy;
                mask |= Permissions.Delete;
                mask |= Permissions.Edit;
                mask |= Permissions.Modify;
            }

            bool isFriend = false;
            bool isFriendOfFriend = false;

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
                    if (isFriend || isOwner)
                    {
                        mask |= Permissions.View;
                        mask |= Permissions.Accept;
                    }
                    break;
                // friend of friend can view and accept 
                case DataAccess.Enums.Privacy.FriendsOfFriend:
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
            return mask;
        }

        public bool IsFriend(Account ab)
        {
            return Friends.Any(a => a == ab.UserId);
        }

        public bool IsFriendOfFriend(Account ab)
        {
            string[] friends = ab.Friends.Split(new char[] { ';' });
            var result = from a in this.Friends
                         where friends.Any(c => c == a)
                         select a;

            return (result.Count() > 0);
        }

        public Permissions GetPermissions(Activity activity)
        {
            Permissions mask = Permissions.EmptyMask;
            bool isOwner = activity.Organizer.Id == this.account.Id;

            if (isOwner)
            {
                mask |= Permissions.Copy;
                mask |= Permissions.Delete;
                mask |= Permissions.Edit;
                mask |= Permissions.Modify;
                mask |= Permissions.InviteGuests;
            }            

            bool isFriend = IsFriend(activity.Organizer);
            bool isFriendOfFriend = IsFriendOfFriend(activity.Organizer);
            bool isInvited = this.EventActions.IsInvited(activity);

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
