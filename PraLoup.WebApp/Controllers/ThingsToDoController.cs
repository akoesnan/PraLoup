using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PraLoup.WebApp.Models;

namespace PraLoup.WebApp.Controllers
{
    public class ThingsToDoController : Controller
    {
        public MetroAreaModel MetroAreaModel { get; private set; }
        public ThingsToDoCityModel ThingsToDoCityModel { get; private set; }

        public ThingsToDoController(MetroAreaModel metroAreaModel, ThingsToDoCityModel ttdCityModel)
        {
            this.MetroAreaModel = metroAreaModel;
            this.ThingsToDoCityModel = ttdCityModel;            
        }

        //
        // GET: /ThingsToDo/  
        public ActionResult Index()
        {            
            return View(this.MetroAreaModel);
        }

        public ActionResult City(string city)
        {
            if (this.ThingsToDoCityModel == null )
            {
                return View("Error");
            }
            else
            {
                if (String.IsNullOrEmpty(city) || !this.MetroAreaModel.IsSupportedMetro(city))
                {
                    return View("Error");
                }
                else
                {
                    this.ThingsToDoCityModel.Construct(city);
                    ViewData.Model = this.ThingsToDoCityModel;
                    return View();
                }
            }
        }
    }
}
