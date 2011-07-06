using System;
using System.Linq;
using Facebook.Web;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Entities;
using PraLoup.Utilities;
using PraLoup.DataAccess.Interfaces;
using Facebook;
﻿

namespace PraLoup.Facebook
{
    public partial class FacebookAccount 
    {
        private OAuthHandler OAuth { get; set; }
        private FacebookClient FbClient { get; set; }
        private Account Account { get; set; }

        private static FacebookAccount _cur = null;
                
        public FacebookAccount(FacebookClient fbClient, Account account)            
        {            
            this.FbClient = fbClient;
            this.Account = account;      
            this.Account = GetFacebookAccount();
        }        

        private Account GetFacebookAccount()
        {
            dynamic fbAccount = FbClient.Get("me");                            
            var acct = new Account();
            acct.FirstName = fbAccount.first_name;
            acct.UserId = fbAccount.id;
            acct.LastName = fbAccount.last_name;
            acct.UserName = fbAccount.name;
            return acct;
        }

        public static void PostToWall(OAuthHandler oAuth)
        {
            // use facebook c# sdk method instead...
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
    }
}