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
                "BusinessController_default",
                "Business/{action}/{id}",
                new { controller = "Business", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "BusinessPromotion_default",
                "Business/Promotion/{action}/{businessId}",
                new { controller = "Promotion", action = "Index", businessId = UrlParameter.Optional }
            );

            context.MapRoute(
                "Business_default",
                "Business/{controller}/{action}/{id}",
                new { controller = "Business", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
