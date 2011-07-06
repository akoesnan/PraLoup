using System;
using System.Web.Mvc;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Entities;
using PraLoup.Facebook;
using PraLoup.Utilities;
using PraLoup.WebApp.Models;
using Facebook.Web;
using PraLoup.DataAccess.Interfaces;
using Facebook;
using PraLoup.BusinessLogic;
using System.Collections;
using PraLoup.Plugins;
using System.Collections.Generic;
using System.Web.Security;

namespace ProjectSafari.Controllers
{
    [HandleError]
    public class AccountController : Controller
    {
        private IRepository Repository { get; set; }
        private IEnumerable<IEventAction> EventActionPlugins { get; set; }

        private AccountBase _accountbase;
        private const string returnUrl = "http://localhost/praloup.webapp/Event";
        private const string logoffUrl = "http://localhost/praloup.webapp/Event";
        private const string redirectUrl = "http://localhost/praloup.webapp/account/OAuth";

        private AccountBase AccountBase
        {
            get
            {
                if (_accountbase == null)
                {
                    _accountbase = new AccountBase(this.Repository, this.EventActionPlugins);
                }
                return _accountbase;
            }
        }

        public AccountController(IRepository repository, IEnumerable<IEventAction> eventActionPlugins)
        {
            this.Repository = repository;
            this.EventActionPlugins = eventActionPlugins;
        }

        public ActionResult Login()
        {
            if (!FacebookWebContext.Current.IsAuthenticated())
            {
                var oAuthClient = new FacebookOAuthClient(FacebookApplication.Current);
                oAuthClient.RedirectUri = new Uri(redirectUrl);
                var loginUri = oAuthClient.GetLoginUrl(new Dictionary<string, object> { { "state", returnUrl } });
                return Redirect(loginUri.AbsoluteUri);
            }
            else
            {
                FacebookWebAuthorizer fwa = new FacebookWebAuthorizer(new PraLoupFacebookApplication(), HttpContext);
                fwa.Permissions = new string[] { "publish_stream" };
                fwa.ReturnUrlPath = returnUrl;
                fwa.CancelUrlPath = returnUrl;
                if (fwa.Authorize())
                {
                    Register();
                    return RedirectToAction("Home", "Home");
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
            var oAuth = new OAuthHandler();

            //Get the access token and secret.
            oAuth.Token = FacebookWebContext.Current.AccessToken;
            if (oAuth.Token.Length > 0)
            {
                bool notregistered = this.AccountBase.IsCreated();

                if (notregistered)
                {
                    this.AccountBase.Register();
                }
            }
            Response.Cookies.Add(new System.Web.HttpCookie("LoggedIn", oAuth.Token));
            return true;
        }

        //
        // GET: /Account/OAuth/
        public ActionResult OAuth(string code, string state)
        {
            FacebookOAuthResult oauthResult;
            if (FacebookOAuthResult.TryParse(Request.Url, out oauthResult))
            {
                if (oauthResult.IsSuccess)
                {
                    var oAuthClient = new FacebookOAuthClient(FacebookApplication.Current);
                    oAuthClient.RedirectUri = new Uri(redirectUrl);
                    dynamic tokenResult = oAuthClient.ExchangeCodeForAccessToken(code);
                    string accessToken = tokenResult.access_token;

                    DateTime expiresOn = DateTime.MaxValue;

                    if (tokenResult.ContainsKey("expires"))
                    {
                        expiresOn = DateTimeConvertor.FromUnixTime(tokenResult.expires);
                    }

                    FacebookClient fbClient = new FacebookClient(accessToken);
                    dynamic me = fbClient.Get("me?fields=id,name");
                    long facebookId = Convert.ToInt64(me.id);

                    this.Repository.Add(new FacebookUser
                    {
                        AccessToken = accessToken,
                        Expires = expiresOn,
                        FacebookId = facebookId,
                        Name = (string)me.name,
                    });

                    FormsAuthentication.SetAuthCookie(facebookId.ToString(), false);

                    // prevent open redirection attack by checking if the url is local.
                    if (Url.IsLocalUrl(state))
                    {
                        return Redirect(state);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
