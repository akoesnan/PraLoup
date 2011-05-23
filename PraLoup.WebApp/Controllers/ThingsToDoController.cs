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
        
        //
        // GET: /ThingsToDo/  
        public ActionResult Index()
        {
            this.MetroAreaModel = new MetroAreaModel();
            this.MetroAreaModel.Construct();

            return View(this.MetroAreaModel);
        }

        public ActionResult City(string city)
        {
            ViewData["Message"] = "Displaying Things to do in a city";
            
            var ttd = new ThingsToDoCityModel(city);
            ttd.Construct();

            ViewData.Model = ttd;
            return View();
        }


    }
}
