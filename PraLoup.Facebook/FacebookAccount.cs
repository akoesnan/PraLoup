using System;
using System.Linq;
using Facebook;
using Facebook.Web;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Entities;
using PraLoup.Utilities;
using PraLoup.BusinessLogic;
﻿

namespace PraLoup.Facebook
{
    public class FacebookAccount : AccountBase
    {
        private static FacebookAccount _cur = null;
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
                           _cur = new FacebookAccount();
                        }
                    }
                }
                return _cur;
            }
        }

        public FacebookAccount()
        {
            FacebookClient fc = new FacebookClient(FacebookWebContext.Current.AccessToken);

            string json = fc.Get("me").ToString();

            HydrateUserFromJson(json);
        }

        public FacebookAccount(string id)
        {
            FacebookClient fc = new FacebookClient(FacebookWebContext.Current.AccessToken);

            string json = fc.Get(id).ToString();

            HydrateUserFromJson(json);
        }

        public void HydrateUserFromJson(string json)
        {
            dynamic jsonobject = json.GetJson();
            account = new Account();
            account.FirstName = jsonobject.first_name;
            account.UserId = jsonobject.id;
            account.LastName = jsonobject.last_name;

            account.UserName = jsonobject.name;
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


    }
}