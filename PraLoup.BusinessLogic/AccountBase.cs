using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using PraLoup.BusinessLogic.Plugins;
using PraLoup.DataAccess.Entities;
using PraLoup.FacebookObjects;
using PraLoup.DataAccess.Services;
using PraLoup.Infrastructure.Logging;

namespace PraLoup.BusinessLogic
{
    public class AccountBase : MembershipUser
    {
        protected Account account = null;

        protected string[] _friends = null;

        protected static object mutex = new object();

        internal IDataService DataService { get; set; }

        public FacebookAccount FacebookAccount { get; set; }
        public ActivityActions ActivityActions { get; private set; }
        public EventActions EventActions { get; private set; }
        public ILogger Log { get; set; }

        public AccountBase(IDataService dataService,
            EventActions eventActions,
            ActivityActions activityActions,
            ILogger log)
        {
            this.DataService = dataService;
            this.EventActions = eventActions;
            this.ActivityActions = activityActions;
            this.Log = log;
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

        private Account FindById(int id)
        {
            return this.DataService.Account.Find(id);
        }

        private Account GetAccountByFacebookId(long userName)
        {
            return this.DataService.Account.FirstOrDefault(a => userName == a.FacebookLogon.FacebookId);
        }

        public void SetupFacebookAccount()
        {
            this.FacebookAccount = new FacebookAccount(this.account);
            var acct = this.GetAccountByFacebookId(this.FacebookAccount.Account.FacebookLogon.FacebookId);
            if (acct == null)
            {
                IEnumerable<string> brokenRules;
                var success = this.DataService.Account.SaveOrUpdate(this.FacebookAccount.Account, out brokenRules);
                if (success)
                {
                    this.DataService.Commit();
                    this.Log.Info("Saved facebook login information");
                }
                else
                {
                    this.Log.Info("Unable to save facebook logon information");
                }
            }
            this.account = this.FacebookAccount.Account;
        }

        protected void Register(bool create)
        {
            IEnumerable<string> brokenRules;
            var success = this.DataService.Account.SaveOrUpdate(account, out brokenRules);
            if (success)
            {
            }
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

        public Permissions GetPermissions(Activity activity)
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
                    isInvited = this.ActivityActions.IsInvited(activity);
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
