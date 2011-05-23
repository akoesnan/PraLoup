using System;
using System.Web.Mvc;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Entities;
using PraLoup.Facebook;
using PraLoup.Utilities;
using PraLoup.WebApp.Models;

namespace ProjectSafari.Controllers
{

    [HandleError]
    public class AccountController : Controller
    {

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(string json) 
        {

            var oAuth = new OAuthHandler();

            dynamic djo = json.GetJson();
            //Get the access token and secret.
            oAuth.Token = djo.sessionkey;
            if (oAuth.Token.Length > 0)
            {
                FacebookAccount fa = new FacebookAccount(oAuth);
                bool notregistered = fa.IsCreated();

                if (notregistered)
                {
                    fa.Register();
                }
            }
            Response.Cookies.Add(new System.Web.HttpCookie("LoggedIn", "true"));
            return null;
        }
    }
}