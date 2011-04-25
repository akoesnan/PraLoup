using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectSafari.Models;

namespace ProjectSafari.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {


        public enum Method { GET, POST };

        public const string AUTHORIZE = "https://graph.facebook.com/oauth/authorize";
        public const string ACCESS_TOKEN = "https://graph.facebook.com/oauth/access_token";
        public const string CALLBACK_URL = "http://projectsafari.com:62789/Register.aspx";
        public const string appkey = "119592781389488";

        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";
            FacebookOAuthHandler foah = new FacebookOAuthHandler();
            HomeModel hm = new HomeModel();
            hm.redirecturl = foah.AuthorizationLinkGet();
            ViewData.Model = hm;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
