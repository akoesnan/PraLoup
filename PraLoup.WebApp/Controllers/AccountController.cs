using System;
using System.Web.Mvc;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Entities;
using PraLoup.FacebookObjects;
using PraLoup.Utilities;
using PraLoup.WebApp.Models;
using Facebook.Web;
using Facebook;
using PraLoup.BusinessLogic;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using PraLoup.BusinessLogic.Plugins;
using PraLoup.DataAccess.Services;

namespace ProjectSafari.Controllers
{
    [HandleError]
    public class AccountController : Controller
    {
        private IDataService DataService { get; set; }
        private IEnumerable<IActivityAction> EventActionPlugins { get; set; }

        private AccountBase AccountBase;
        private const string returnUrl = "http://localhost/praloup.webapp/Event";
        private const string logoffUrl = "http://localhost/praloup.webapp/Event";
        private const string redirectUrl = "http://localhost/praloup.webapp/account/OAuth";

        public AccountController(AccountBase accountBase, IDataService dataService)
        {
            this.AccountBase = accountBase;
            this.DataService = DataService;
        }

        public ActionResult Login()
        {
            if (!FacebookWebContext.Current.IsAuthenticated())
            {
                return View();
            }
            else
            {
                FacebookWebAuthorizer fwa = new FacebookWebAuthorizer(new PraLoupFacebookApplication(), HttpContext);
                fwa.Permissions = new string[] { "publish_stream", "user_about_me", "read_friendlists","user_photos","friends_photos" };
                fwa.ReturnUrlPath = returnUrl;
                fwa.CancelUrlPath = returnUrl;
                if (fwa.Authorize())
                {
                    Register();
                    string url;
                    if (FacebookWebContext.Current.HttpContext.Request.UrlReferrer != null)
                    {
                        url = FacebookWebContext.Current.HttpContext.Request.UrlReferrer.ToString();
                    }
                    else
                    {
                        url = returnUrl;
                    }
                    return Redirect(url);
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
            // Merge issue?
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
