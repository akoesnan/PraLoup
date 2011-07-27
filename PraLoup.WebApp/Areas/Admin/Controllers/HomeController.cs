using System.Web.Mvc;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Services;
using PraLoup.Infrastructure.Logging;
using PraLoup.WebApp.Areas.Admin.Models;
using PraLoup.WebApp.Utilities;

namespace PraLoup.WebApp.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private IDataService DataService;
        private ILogger Log;

        public HomeController(IDataService dataService, ILogger log)
        {
            this.DataService = dataService;
            this.Log = log;
        }

        //
        // GET: /Admin/Home/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [UnitOfWork]
        public ActionResult CreateTestData()
        {
            var g = new TestSeedDataGenerator(DataService, Log);
            g.Seed();
            return View("Index", new AdminHomePageModel() { Message = "TestDataSeedGenerator has been executed" });
        }
    }
}
