using System;
using System.Linq;
using System.Web.Security;
using Facebook.Web;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Entities;
using PraLoup.Utilities;
﻿

namespace PraLoup.Facebook
{
    public class FacebookAccount : MembershipUser
    {
        private Account account = null;
        private static FacebookAccount _cur = null;
        private static object mutex = new object();
        public static FacebookAccount Current
        {
            get
            {
                if (_cur == null)
                {
                    lock (mutex)
                    {
                        if (_cur == null)
                        {
                            var oAuth = new OAuthHandler();

                            //Get the access token and secret.
                            oAuth.Token = FacebookWebContext.Current.AccessToken;
                            _cur = new FacebookAccount(oAuth);
                        }
                    }
                }
                return _cur;
            }
        }

        public FacebookAccount(OAuthHandler oAuth)
        {

            string url = "https://graph.facebook.com/me/?access_token=" + oAuth.Token;
            string json = oAuth.WebRequest(OAuthHandler.Method.GET, url, String.Empty);
            dynamic jsonobject = json.GetJson();
            account = new Account();
            account.FirstName = jsonobject.first_name;
            account.FacebookId = jsonobject.id;
            account.LastName = jsonobject.last_name;

            account.UserName = jsonobject.name;

        }

        public FacebookAccount(OAuthHandler oAuth, string id)
        {
            string url = "https://graph.facebook.com/" + id + "/?access_token=" + oAuth.Token;
            string json = oAuth.WebRequest(OAuthHandler.Method.GET, url, String.Empty);
            dynamic jsonobject = json.GetJson();
            account = new Account();
            account.FirstName = jsonobject.first_name;
            account.FacebookId = jsonobject.id;
            account.LastName = jsonobject.last_name;

            account.UserName = jsonobject.name;
        }

        public bool IsCreated()
        {
            EntityRepository er = new EntityRepository();

            var result = from a in er.Accounts
                         where
                             a.FacebookId == account.FacebookId
                         select a;

            if (result.Count<Account>() == 0)
            {
                return true;
            }
            return false;
        }

        public void Register()
        {
            GenericRepository gr = new GenericRepository(new EntityRepository());
            gr.Add<Account>(account);
            gr.SaveChanges();
        }

        public Account GetAccount()
        {
            return this.account;
        }

        public static void PostToWall(OAuthHandler oAuth)
        {
            var ptw = new Wall();
            ptw.Message = "blank";
            ptw.Action = "action";
            ptw.Caption = "caption";
            ptw.Description = "description";
            ptw.Link = "http://projectsafari.org";
            ptw.Name = "name";
            ptw.Source = "http://www.google.com/images/logos/ps_logo2.png";
            ptw.AccessToken = oAuth.Token;

            ptw.Post();
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
                case DataAccess.Enums.Privacy.Public:
                    mask |= Permissions.View;
                    mask |= Permissions.Share;
                    mask |= Permissions.Accept;
                    break;
                case DataAccess.Enums.Privacy.Friends:
                    if (isFriend || isOwner)
                    {
                        mask |= Permissions.View;
                        mask |= Permissions.Accept;
                    }
                    break;
                case DataAccess.Enums.Privacy.FriendsOfFriend:
                    if (isFriendOfFriend || isFriend || isOwner)
                    {
                        mask |= Permissions.View;
                        mask |= Permissions.Accept;
                    }
                    break;
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

        public Permissions GetPermissions(Activity e)
        {
            Permissions mask = Permissions.EmptyMask;
            bool isOwner = e.Organizer.Id == this.account.Id;

            if (isOwner)
            {
                mask |= Permissions.Copy;
                mask |= Permissions.Delete;
                mask |= Permissions.Edit;
                mask |= Permissions.Modify;
                mask |= Permissions.InviteGuests;
            }

            bool isFriend = false;
            bool isFriendOfFriend = false;
            bool isInvited = false;

            if (isInvited)
            {
                mask |= Permissions.Accept;
                mask |= Permissions.View;
                mask |= Permissions.Copy;
            }

            // for activities, privacy means who can invite people to the activity.
            switch (e.Privacy)
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
            bool isOwner = e.Recipients.Any(x => x.Id == this.account.Id);
            bool isActivtyOwner = e.Activity.Organizer.Id == this.account.Id;

            if (isOwner || isActivtyOwner)
            {
                mask |= Permissions.Delete;
            }
            return Permissions.EmptyMask;
        }
    }
}