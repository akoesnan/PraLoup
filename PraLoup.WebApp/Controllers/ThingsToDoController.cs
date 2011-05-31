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
        MetroAreaModel MetroAreaModel { get; set; }
        ThingsToDoCityModel ThingsToDoCityModel { get; set; }

        public ThingsToDoController(MetroAreaModel metroAreaModel, ThingsToDoCityModel ttdCityModel)
        {
            this.MetroAreaModel = metroAreaModel;
            this.ThingsToDoCityModel = ttdCityModel;            
        }

        //
        // GET: /ThingsToDo/  
        public ActionResult Index()
        {            
            this.MetroAreaModel.Construct();
            return View(this.MetroAreaModel);
        }

        public ActionResult City(string city)
        {            
            this.ThingsToDoCityModel.Construct(city);
            ViewData.Model = this.ThingsToDoCityModel;
            return View();
        }
    }
}
