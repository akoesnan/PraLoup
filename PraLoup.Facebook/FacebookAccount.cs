using System;
using System.Linq;
using System.Web.Security;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Entities;
using PraLoup.Utilities;


namespace PraLoup.Facebook
{

    public class FacebookAccount : MembershipUser
    {

        private Account account = null;
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
            string url = "https://graph.facebook.com/"+id+"/?access_token=" + oAuth.Token;
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
    }
}