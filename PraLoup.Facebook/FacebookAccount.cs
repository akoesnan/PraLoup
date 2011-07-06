using System;
using System.Dynamic;
using System.Linq;
using Facebook;
using Facebook.Web;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Entities;
using PraLoup.Utilities;
using PraLoup.DataAccess.Enums;

namespace PraLoup.FacebookObjects
{
    public class FacebookAccount 
    {
        public Account Account { get; set; }        

        public FacebookAccount(Account account)
        {
            this.Account = new Account() ;

            FacebookClient fc = new FacebookClient(FacebookWebContext.Current.AccessToken);

            dynamic jsonobject = fc.Get("me");

            HydrateUserFromJson(jsonobject);
        }

        public FacebookAccount(string id)
        {
            FacebookClient fc = new FacebookClient(FacebookWebContext.Current.AccessToken);

            dynamic json = fc.Get(id);

            HydrateUserFromJson(json);
        }

        public void HydrateUserFromJson(dynamic jsonobject)
        {                    
            this.Account.FirstName = jsonobject.first_name;
            this.Account.UserId = jsonobject.id;
            this.Account.LastName = jsonobject.last_name;
            this.Account.UserName = jsonobject.name;            
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

        public static void PostToWall(Activity a)
        {
            FacebookClient fc = new FacebookClient(FacebookWebContext.Current.AccessToken);
            dynamic parameters = PopulateParametersForEvent(a.Event);
            parameters.actions = new
            {
                name = "View on Wildfire",
                link = "http://www.projectsafari.com/"
            };
            SetPrivacy(parameters, a.Privacy);
            dynamic result = fc.Post("me/feed", parameters);
        }

        public void InviteFriend(Activity e, FacebookAccount f)
        {

        }

        public void SetPrivacy(dynamic obj, Privacy p)
        {
            switch(p )
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