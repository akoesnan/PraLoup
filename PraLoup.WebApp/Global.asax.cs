using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PraLoup.DataAccess;
using FluentNHibernate.Automapping;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Mapping;
using NHibernate;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using System.IO;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;

namespace PraLoup.WebApp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "ThingsToDoInCity", // Route name
                "ThingsToDo/{city}", // URL with parameters
                new { controller = "ThingsToDo", action = "City" } // Parameter defaults
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new[] { "PraLoup.WebApp.Controllers" }
            );
        }

        /// <summary>
        /// Called when the application is started.
        /// </summary>
        public void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);            
        }        
    }
}