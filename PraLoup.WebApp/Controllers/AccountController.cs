using System;
using System.Web.Mvc;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Entities;
using PraLoup.Facebook;
using PraLoup.Utilities;
using PraLoup.WebApp.Models;
using Facebook.Web;

namespace ProjectSafari.Controllers
{

    [HandleError]
    public class AccountController : Controller
    {
         public ActionResult Login()
         {
             
             if (FacebookWebContext.Current.IsAuthenticated())
             {
                 FacebookWebAuthorizer fwa = new FacebookWebAuthorizer(new PraLoupFacebookApplication(), HttpContext);
                 fwa.Permissions = new string[]{"publish_stream"};
                 fwa.ReturnUrlPath = HttpContext.Request.Url.ToString();
                 if (fwa.Authorize())
                 {
                     Register();
                     return RedirectToAction("About", "Home");
                 }
             }

             return View();
         }

        
        public bool Register() 
        {
            var oAuth = new OAuthHandler();

            //Get the access token and secret.
            oAuth.Token = FacebookWebContext.Current.AccessToken;
            if (oAuth.Token.Length > 0)
            {
                FacebookAccount fa = new FacebookAccount(oAuth);
                bool notregistered = fa.IsCreated();

                if (notregistered)
                {
                    fa.Register();
                }
            }
            Response.Cookies.Add(new System.Web.HttpCookie("LoggedIn", oAuth.Token));
            return true;
        }
    }
}