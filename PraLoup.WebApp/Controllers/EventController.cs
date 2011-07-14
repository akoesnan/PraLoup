using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using Facebook.Web.Mvc;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Entities;
using PraLoup.FacebookObjects;
using PraLoup.WebApp.Models;
using PraLoup.BusinessLogic;
using PraLoup.DataAccess.Interfaces;
using System.Collections.Generic;
using PraLoup.Plugins;
using PraLoup.DataAccess.Enums;

namespace PraLoup.WebApp.Controllers
{
    public class EventController : Controller
    {
        IRepository Repository { get; set; }
        AccountBase AccountBase { get; set; }

        public EventController(IRepository repository, IEnumerable<IEventAction> eventActionPlugins)
        {
            this.Repository = repository;
            this.AccountBase = new AccountBase(this.Repository, eventActionPlugins);
        }

        //
        // GET: /Event/        
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult Index()
        {
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
        }

        //
        // GET: /Event/Details/5
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult Details(int id)
        {
            var o = Repository.Find<Event>(id);
            EventModel em = new EventModel();
            em.Event = o;
            em.Permissions = this.AccountBase.GetPermissions(o);
            if (!em.Permissions.HasFlag(Permissions.View))
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
                    Repository.Add(e);
                    Repository.SaveChanges();

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
            EventModel em = new EventModel();
            var e = Repository.Find<Event>(id);
            if (e == null)
            {
                // TODO: what to do when there is no such event
                return RedirectToAction("Index");
            }

            em.Event = e;
            em.Permissions = this.AccountBase.GetPermissions(e);

            if (!em.Permissions.HasFlag(Permissions.Edit))
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

