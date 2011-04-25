using System;
using System.Web.Mvc;
using ProjectSafari.Models;
using ProjectSafari.Utilities;
using ProjectSafari.Facebook;

namespace ProjectSafari.Controllers
{

    [HandleError]
    public class AccountController : Controller
    {
        public ActionResult Register()
        {
            string url = "";
            FacebookOAuthHandler oAuth = new FacebookOAuthHandler();

            if (Request["code"] == null)
            {
                //Redirect the user back to Facebook for authorization.
                Response.Redirect(oAuth.AuthorizationLinkGet());
            }
            else
            {
                //Get the access token and secret.
                oAuth.AccessTokenGet(Request["code"]);

                if (oAuth.Token.Length > 0)
                {
                    //We now have the credentials, so we can start making API calls
                    url = "https://graph.facebook.com/me/?access_token=" + oAuth.Token;
                  //  url = "https://graph.facebook.com/me/name?access_token=" + oAuth.Token;
                    string json = oAuth.WebRequest(FacebookOAuthHandler.Method.GET, url, String.Empty);
                    dynamic jsonobject = JsonFromString.Get(json);
                    RegisterModel rm = new RegisterModel(); 
                    rm.first_name = jsonobject.first_name;
                    rm.id = jsonobject.id;
                    rm.last_name = jsonobject.last_name;
                    rm.middle_name = jsonobject.middle_name;
                    rm.name = jsonobject.name;
                    ViewData.Model = rm;
                    PostToWall ptw = new PostToWall();
                    ptw.Message = "blank";
                    ptw.Action = "action";
                    ptw.Caption = "caption";
                    ptw.Description = "description";
                    ptw.Link = "http://projectsafari.org";
                    ptw.Name = "name";
                    ptw.Source = "http://www.google.com/images/logos/ps_logo2.png";
                    ptw.AccessToken = oAuth.Token;
                  
                    ptw.Post();
                }
            }

            return View();
        }
    }
}
