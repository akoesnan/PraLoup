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
                 fwa.Permissions = new string[]{"publish_stream","user_about_me","read_friendlists"};
                 fwa.ReturnUrlPath = HttpContext.Request.Url.ToString();
                 if (fwa.Authorize())
                 {
                     Facebook.FacebookClient fc = new Facebook.FacebookClient(FacebookWebContext.Current.AccessToken);
                     fc.Get("me");
                     Register();
                     return RedirectToAction("Home", "Home");
                 }
             }

             return View();
         }

        
        public bool Register() 
        {
            FacebookAccount fa = new FacebookAccount();
            bool notregistered = fa.IsCreated();

            if (notregistered)
            {
                fa.Register();
            }
            return true;
        }
    }
}
