using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Facebook.Web.Mvc;
using PraLoup.BusinessLogic;
using PraLoup.BusinessLogic.Plugins;
using PraLoup.DataAccess.Entities;
using PraLoup.WebApp.Models;
using PraLoup.DataAccess.Services;
using PraLoup.Infrastructure.Logging;

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
<<<<<<< HEAD
            this.Repository.Context.Database.Connection.Open();
            var entities = this.Repository.GetAll<Event>();
            List<EventModel> ens = new List<EventModel>();
            foreach (var en in entities)
            {
                EventModel em = new EventModel();
                em.Permissions = this.AccountBase.GetPermissions(en);
                em.Event = en;
                ens.Add(em);
            }
            return View(ens);
=======
            // TODO: validate that this generate sql that execute permission on the sql level
            var events = this.AccountBase.EventActions.GetAllEvents()
                .Where(e => this.AccountBase.GetPermissions(e) == Permissions.View).Take(10)
                .Select(e => new EventModel(e, this.AccountBase.GetPermissions(e)));
            return View(events);
>>>>>>> af941e6edca84663c9fa24559ad630c88a5f1047
        }

        //
        // GET: /Event/Details/5
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult Details(int id)
        {
            var o = DataService.Event.Find(id);
            EventModel em = new EventModel(o, this.AccountBase.GetPermissions(o));
            if (!em.CanView)
            {
                // TODO: what to do when the user doesn't have perms
                return RedirectToAction("Index");
            }
            return View(em);
        }

        //
        // GET: /Event/Create
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Event/Create
        [HttpPost]
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult Create(Event e)
        {
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

            var e = DataService.Event.Find(id);
            if (e == null)
            {
                // TODO: what to do when there is no such event
                return RedirectToAction("Index");
            }
            EventModel em = new EventModel(e, this.AccountBase.GetPermissions(e));

            if (!em.CanEdit)
            {
                // TODO: what to do when the user doesn't have perms
                return RedirectToAction("Index");
            }

            return View(em);
        }

        //
        // POST: /Event/Edit/5
        [HttpPost]
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult Edit(int id, FormCollection collection)
        {
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

