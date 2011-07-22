using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Web.Security;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Services;
using PraLoup.FacebookObjects;
using PraLoup.Infrastructure.Logging;

namespace PraLoup.BusinessLogic
{
    public class AccountBase : MembershipUser
    {
        public Account Account { get; protected set; }

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

            //this.account = this.GetFacebookAccount();
            //this.SetActionsAccount(this.account);
        }

        public void SetupActionAccount()
        {
            this.SetupFacebookAccount();
            this.SetActionsAccount(this.Account);
        }

        private void SetActionsAccount(Account account)
        {
            this.EventActions.Account = account;
            this.ActivityActions.Account = account;
        }

        private Account FindById(int id)
        {
            return this.DataService.Account.Find(id);
        }

        private Account GetAccountByFacebookId(long userName)
        {
            return this.DataService.Account.FirstOrDefault(a => userName == a.FacebookLogon.FacebookId);
        }

        public Account SetupFacebookAccount()
        {
            Contract.Assert(FacebookAccount.HasFacebookAccessToken);
            this.FacebookAccount = new FacebookAccount();
            var acct = this.GetAccountByFacebookId(this.FacebookAccount.Account.FacebookLogon.FacebookId);
            acct = acct ?? this.FacebookAccount.Account;

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
            this.Account = acct;
            return acct;
        }

        public Account GetAccount()
        {
            return this.Account;
        }

        //public PermissionEnum GetPermissions(Event e)
        //{
        //    PermissionEnum mask = PermissionEnum.EmptyMask;
        //    bool isOwner = false;
        //    bool isFriend = false;
        //    bool isFriendOfFriend = false;

        //    if (e.Organizers != null)
        //    {
        //        isOwner = e.Organizers.Any(x => x.Id == this.Account.Id);
        //        foreach (var x in e.Organizers)
        //        {
        //            isFriend |= Account.IsFriend(x);
        //            isFriendOfFriend |= Account.IsFriendOfFriend(x);
        //        }
        //    }

        //    if (isOwner)
        //    {
        //        mask |= PermissionEnum.Copy;
        //        mask |= PermissionEnum.Delete;
        //        mask |= PermissionEnum.Edit;
        //    }


        //    switch (e.Privacy)
        //    {
        //        // public event can be viewed, shared and accpeted by everyone 
        //        case DataAccess.Enums.Privacy.Public:
        //            mask |= PermissionEnum.View;
        //            mask |= PermissionEnum.Share;
        //            mask |= PermissionEnum.Accept;
        //            break;
        //        // friend only event can be viewed and accpeted by friends and owner only
        //        case DataAccess.Enums.Privacy.Friends:
        //            if (e.Organizers != null)
        //            {
        //                foreach (var x in e.Organizers)
        //                {
        //                    isFriend |= Account.IsFriend(x);
        //                }
        //            }
        //            if (isFriend || isOwner)
        //            {
        //                mask |= PermissionEnum.View;
        //                mask |= PermissionEnum.Accept;
        //            }
        //            break;
        //        // friend of friend can view and accept 
        //        case DataAccess.Enums.Privacy.FriendsOfFriend:
        //            if (e.Organizers != null)
        //            {
        //                foreach (var x in e.Organizers)
        //                {
        //                    isFriend |= Account.IsFriend(x);
        //                    isFriendOfFriend |= Account.IsFriendOfFriend(x);
        //                }
        //            }
        //            if (isFriendOfFriend || isFriend || isOwner)
        //            {
        //                mask |= PermissionEnum.View;
        //                mask |= PermissionEnum.Accept;
        //            }
        //            break;
        //        // private means that only the owner can view and and accept
        //        // Todo: how about people that are invited explicitly?
        //        case DataAccess.Enums.Privacy.Private:
        //            if (isOwner)
        //            {
        //                mask |= PermissionEnum.View;
        //                mask |= PermissionEnum.Accept;
        //            }
        //            break;
        //    }
        //    mask |= PermissionEnum.View;
        //    return mask;
        //}

        //public PermissionEnum GetPermissions(Activity activity)
        //{
        //    PermissionEnum mask = PermissionEnum.EmptyMask;
        //    bool isOwner = false;
        //    bool isFriend = false;
        //    bool isFriendOfFriend = false;
        //    bool isInvited = false;

        //    if (activity.Organizer != null)
        //    {
        //        isOwner = activity.Organizer.Id == this.Account.Id;
        //        if (!isOwner)
        //        {
        //            isFriend = Account.IsFriend(activity.Organizer);
        //            isFriendOfFriend = Account.IsFriendOfFriend(activity.Organizer);
        //            isInvited = this.ActivityActions.IsInvited(activity);
        //        }
        //    }

        //    if (isOwner)
        //    {
        //        mask |= PermissionEnum.Copy;
        //        mask |= PermissionEnum.Delete;
        //        mask |= PermissionEnum.Edit;
        //        mask |= PermissionEnum.InviteGuests;
        //    }

        //    if (isInvited)
        //    {
        //        mask |= PermissionEnum.Accept;
        //        mask |= PermissionEnum.View;
        //        mask |= PermissionEnum.Copy;
        //    }

        //    // for activities, privacy means who can invite people to the activity.
        //    switch (activity.Privacy)
        //    {
        //        case DataAccess.Enums.Privacy.Public:
        //            mask |= PermissionEnum.View;
        //            mask |= PermissionEnum.Share;
        //            mask |= PermissionEnum.Accept;
        //            mask |= PermissionEnum.InviteGuests;
        //            break;
        //        case DataAccess.Enums.Privacy.Friends:
        //            if (isFriend && isInvited)
        //            {
        //                mask |= PermissionEnum.InviteGuests;
        //            }

        //            break;
        //        case DataAccess.Enums.Privacy.FriendsOfFriend:
        //            if ((isFriendOfFriend || isFriend) && isInvited)
        //            {
        //                mask |= PermissionEnum.InviteGuests;
        //            }
        //            break;
        //        case DataAccess.Enums.Privacy.Private:
        //            break;
        //    }
        //    if (mask != PermissionEnum.EmptyMask)
        //    {
        //        mask |= PermissionEnum.View;
        //    }
        //    return mask;
        //}

        //public PermissionEnum GetPermissions(InvitationResponse e)
        //{
        //    PermissionEnum mask = PermissionEnum.EmptyMask;
        //    bool isOwner = false;
        //    bool isActivtyOwner = false;

        //    if (isOwner || isActivtyOwner)
        //    {
        //        mask |= PermissionEnum.Delete;
        //    }

        //    return mask;
        //}

        //public PermissionEnum GetPermissions(Invitation e)
        //{
        //    PermissionEnum mask = PermissionEnum.EmptyMask;
        //    bool isOwner = e.Recipient == this.Account;
        //    bool isActivtyOwner = e.Activity.Organizer == this.Account;

        //    if (isOwner || isActivtyOwner)
        //    {
        //        mask |= PermissionEnum.Delete;
        //    }
        //    return PermissionEnum.EmptyMask;
        //}
    }
}
