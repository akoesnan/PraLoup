using System;
using System.Web.Mvc;
using System.Web.Security;
using Facebook;
using Facebook.Web;
using PraLoup.BusinessLogic;
using PraLoup.FacebookObjects;
using PraLoup.WebApp.Utilities;

namespace ProjectSafari.Controllers
{
    [HandleError]
    public class AccountController : Controller
    {        
        private AccountBase AccountBase;
        private const string returnUrl = "http://localhost/praloup.webapp/";
        private const string logoffUrl = "http://localhost/praloup.webapp/";
        private const string redirectUrl = "http://localhost/praloup.webapp/account/OAuth";

        public AccountController(AccountBase accountBase)
        {
            this.AccountBase = accountBase;            
        }
        [UnitOfWork]
        public ActionResult Login()
        {
            if (!FacebookWebContext.Current.IsAuthenticated())
            {
                return View();
            }
            else
            {
                try
                {
                    Register();
                }
                catch (Exception)
                {
                    return View();
                }
                FacebookWebAuthorizer fwa = new FacebookWebAuthorizer(new PraLoupFacebookApplication(), HttpContext);
                fwa.Permissions = new string[] { "publish_stream", "user_about_me", "read_friendlists","user_photos","friends_photos" };
                fwa.ReturnUrlPath = returnUrl;
                fwa.CancelUrlPath = returnUrl;
                if (fwa.Authorize())
                {
                    string url = null;
                    if (FacebookWebContext.Current.HttpContext.Request.UrlReferrer != null)
                    {
                        url = FacebookWebContext.Current.HttpContext.Request.UrlReferrer.ToString();
                    }
                    
                    if (string.IsNullOrEmpty(url))
                    {
                        return RedirectToAction("Create", "Promotion", new { area = "Business" });
                    }
                    else
                    {
                        return Redirect(url);
                    }
                }
                return View();
            }
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            var oAuthClient = new FacebookOAuthClient();
            oAuthClient.RedirectUri = new Uri(logoffUrl);
            var logoutUrl = oAuthClient.GetLogoutUrl();
            return Redirect(logoutUrl.AbsoluteUri);
        }

        public bool Register()
        {
            this.AccountBase.SetupFacebookAccount();
            return true;
        }

        //
        // GET: /Account/OAuth/
        [HttpPost]
        public ActionResult OAuth(string access_token, string expires)
        {
            DateTime expiresOn = DateTime.MaxValue;
            if (!String.IsNullOrEmpty(expires) && expires != "0")
            {
                expiresOn = DateTimeConvertor.FromUnixTime(expires);
            }

            FacebookClient fbClient = new FacebookClient(access_token);
            dynamic me = fbClient.Get("me?fields=id,name");
            long facebookId = Convert.ToInt64(me.id);
            
            // TODO: add this to the account 
            //this.Repository.Add(new FacebookUser
            //{
            //    AccessToken = accessToken,
            //    Expires = expiresOn,
            //    FacebookId = facebookId,
            //    Name = (string)me.name,
            //});

            FormsAuthentication.SetAuthCookie(facebookId.ToString(), false);

            // prevent open redirection attack by checking if the url is local.
            return View();
        }

    }
}
