using System.Web;
using System.Web.Mvc;
using Facebook.Web;
using Facebook.Web.Mvc;
using PraLoup.FacebookObjects;

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
            FacebookWebAuthorizer fwa = new FacebookWebAuthorizer(new PraLoupFacebookApplication(), HttpContext);
            fwa.Permissions = new string[]{"publish_stream"};
            fwa.ReturnUrlPath = HttpContext.Request.Url.ToString();
            if (fwa.Authorize())
            {
                var oAuth = new OAuthHandler();

                //Get the access token and secret.
                oAuth.Token = FacebookWebContext.Current.AccessToken;
                Friends f = new Friends(oAuth);
                FriendsLists fl = new FriendsLists(oAuth);
            
            }
            
            return View();
        }
    }
}
