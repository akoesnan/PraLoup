using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PraLoup.Facebook;
using PraLoup.WebApp.Models;
using Facebook.Web.Mvc;

namespace PraLoup.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public enum Method { GET, POST };

        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";
            return View();
        }

        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult About()
        {
            return View();
        }
    }
}
