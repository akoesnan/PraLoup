using System;
using System.Dynamic;
using Facebook;
using Facebook.Web;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;

namespace PraLoup.FacebookObjects
{
    public class FacebookAccount
    {
        public Account Account { get; set; }

        public FacebookAccount()
        {
            if (HasFacebookAccessToken)
            {
                GetFacebookLoginInformation("me");
            }
        }

        public FacebookAccount(string id)
        {
            if (HasFacebookAccessToken)
            {
                GetFacebookLoginInformation(id);
            }
        }

        public void GetFacebookLoginInformation(string id)
        {
            FacebookClient fc = new FacebookClient(FacebookWebContext.Current.AccessToken);

            dynamic json = fc.Get(id);
            // need to instantiate a new account for this user
            this.Account = new Account();
            HydrateUserFromJson(json);
        }

        public static bool HasFacebookAccessToken
        {
            get
            {
                return !String.IsNullOrEmpty(FacebookWebContext.Current.AccessToken);
            }
        }

        public void HydrateUserFromJson(dynamic jsonobject)
        {
            this.Account.FirstName = jsonobject.first_name;
            this.Account.LastName = jsonobject.last_name;
            this.Account.UserName = jsonobject.name;
            this.Account.FacebookLogon = new FacebookLogon();
            this.Account.FacebookLogon.AccessToken = FacebookWebContext.Current.AccessToken;
            this.Account.FacebookLogon.Expires = FacebookWebContext.Current.Session.Expires;
            this.Account.FacebookLogon.FacebookId = FacebookWebContext.Current.UserId;
        }

        public static void PostToWall(Event e)
        {
            FacebookClient fc = new FacebookClient(FacebookWebContext.Current.AccessToken);
            dynamic parameters = PopulateParametersForEvent(e);
            parameters.actions = new
            {
                name = "View on Wildfire",
                link = "http://www.projectsafari.com/"
            };
            SetPrivacy(parameters, e.Privacy);
            dynamic result = fc.Post("me/feed", parameters);
        }

        public static void PostToWall(PromotionInstance a)
        {
            FacebookClient fc = new FacebookClient(FacebookWebContext.Current.AccessToken);
            dynamic parameters = PopulateParametersForEvent(a.Promotion.Event);
            parameters.actions = new
            {
                name = "View on Wildfire",
                link = "http://www.projectsafari.com/"
            };
            SetPrivacy(parameters, a.Promotion.Event.Privacy);
            dynamic result = fc.Post("me/feed", parameters);
        }

        public void SetPrivacy(dynamic obj, Privacy p)
        {
            switch (p)
            {
                case Privacy.Public:
                    {
                        obj.privacy = new
                        {
                            value = "EVERYONE",
                        };
                        break;
                    }
                case Privacy.FriendsOfFriend:
                    {
                        obj.privacy = new
                        {
                            value = "ALL_FRIENDS",
                        };
                        break;
                    }
                case Privacy.Friends:
                    {
                        obj.privacy = new
                        {
                            value = "ALL_FRIENDS",
                        };
                        break;
                    }
                case Privacy.Private:
                    {
                        obj.privacy = new
                        {
                            value = "SELF",
                        };
                        break;
                    }
            }
        }

        protected static dynamic PopulateParametersForEvent(Event e)
        {
            dynamic parameters = new ExpandoObject();
            parameters.message = e.Venue;
            parameters.link = e.Url;
            parameters.picture = "";
            parameters.name = e.Name;
            parameters.caption = e.Description;
            parameters.description = e.Description;
            parameters.targeting = new
            {
                countries = "US",
                regions = "6,53",
                locales = "6",
            };

            return parameters;
        }
    }
}