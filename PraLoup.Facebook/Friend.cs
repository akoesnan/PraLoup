using System;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Entities;
using PraLoup.Utilities;
using System.Linq;
using System.Web.Security;
using Facebook;
using Facebook.Web;


namespace PraLoup.Facebook
{
    public class Friends
    {
        public Friends(OAuthHandler oAuth)
        {
            string url = "https://graph.facebook.com/me/friends?access_token=" + oAuth.Token;
            string json = oAuth.WebRequest(OAuthHandler.Method.GET, url, String.Empty);
        }
    }

    public class FriendsLists
    {
        public FriendsLists(OAuthHandler oAuth)
        {
            string url = "https://graph.facebook.com/me/friendlists?access_token=" + oAuth.Token;
            string json = oAuth.WebRequest(OAuthHandler.Method.GET, url, String.Empty);
        }
    }
}
