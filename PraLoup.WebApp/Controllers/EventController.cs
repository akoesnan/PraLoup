using System.Collections.Generic;
using System.Web.Mvc;
using Facebook.Web.Mvc;
using PraLoup.BusinessLogic;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Services;
using PraLoup.Infrastructure.Logging;
using PraLoup.WebApp.Models;

namespace PraLoup.WebApp.Controllers
{
    public class EventController : Controller
    {
        IDataService DataService { get; set; }
        AccountBase AccountBase { get; set; }

        public EventController(AccountBase accountBase, IDataService dataService, ILogger logger)
        {
            this.DataService = dataService;
            this.AccountBase = accountBase;
        }

        //
        // GET: /Event/        
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult Index()
        {
            this.AccountBase.SetupActionAccount();

            var publicEvents = this.AccountBase.EventActions.GetPublicEvents(0, 10);
            return View(publicEvents);
        }

        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult Discovery()
        {
            this.AccountBase.SetupActionAccount();
            var ed = new EventDiscoveryModel(this.AccountBase);
            return View(ed);
        }

        //
        // GET: /Event/Details/5
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult Details(int id)
        {
            this.AccountBase.SetupActionAccount();

            var e = this.AccountBase.EventActions.GetEvent(id);
            if (!e.Permission.CanView)
            {
                // TODO: what to do when the user doesn't have perms
                return RedirectToAction("Index");
            }
            return View(e);
        }

        //
        // GET: /Event/Create
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult Create()
        {
            this.AccountBase.SetupActionAccount();
            return View();
        }

        //
        // POST: /Event/Create
        [HttpPost]
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult Create(Event e)
        {
            this.AccountBase.SetupActionAccount();
            try
            {
                if (ModelState.IsValid)
                {
                    IEnumerable<string> brokenRules;
                    var success = DataService.Event.SaveOrUpdate(e, out brokenRules);
                    if (success)
                    {
                        DataService.Commit();
                    }
                    else
                    {

                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    // TODO: create a page that says events is added succesfully
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Event/Edit/5
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult Edit(int id)
        {
            this.AccountBase.SetupActionAccount();

            var e = this.AccountBase.EventActions.GetEvent(id);
            if (e == null)
            {
                // TODO: what to do when there is no such event
                return RedirectToAction("Index");
            }

            if (!e.Permission.CanEdit)
            {
                // TODO: what to do when the user doesn't have perms
                return RedirectToAction("Index");
            }

            return View(e);
        }

        //
        // POST: /Event/Edit/5
        [HttpPost]
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            this.AccountBase.SetupActionAccount();
            // Todo: perms?
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Event/Delete/5
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult Delete(int id)
        {
            // TODO: Perms
            return View();
        }

        //
        // POST: /Event/Delete/5
        [HttpPost]
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                // TODO: Perms
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

