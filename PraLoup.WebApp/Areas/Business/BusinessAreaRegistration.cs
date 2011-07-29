using System.Web.Mvc;

namespace PraLoup.WebApp.Areas.Business
{
    public class BusinessAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Business";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Business_default",
                "Business/{controller}/{action}/{id}",
                new { controller = "Business", action = "Index", id = UrlParameter.Optional }
                , new string[]{"Praloup.WebApp.Areas.Business.Controllers"}
            );
        }
    }
}
