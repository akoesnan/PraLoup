using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Facebook;
using Facebook.Web;
using PraLoup.BusinessLogic;
using PraLoup.BusinessLogic.Plugins;
using PraLoup.DataAccess.Services;
using PraLoup.FacebookObjects;

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
                var oAuthClient = new FacebookOAuthClient(FacebookApplication.Current);
                oAuthClient.RedirectUri = new Uri(redirectUrl);
                var loginUri = oAuthClient.GetLoginUrl(new Dictionary<string, object> { { "state", returnUrl } });
                return Redirect(loginUri.AbsoluteUri);
            }
            else
            {
                FacebookWebAuthorizer fwa = new FacebookWebAuthorizer(new PraLoupFacebookApplication(), HttpContext);
                fwa.Permissions = new string[] { "publish_stream", "user_about_me", "read_friendlists" };
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
            this.AccountBase.SetupFacebookAccount();
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
