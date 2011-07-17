using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Facebook.Web.Mvc;
using PraLoup.BusinessLogic;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;
using PraLoup.DataAccess.Services;
using PraLoup.FacebookObjects;
using PraLoup.WebApp.Models;

namespace PraLoup.WebApp.Controllers
{
    public class ActivityController : Controller
    {
        private IDataService DataService;
        private AccountBase AccountBase;

        public ActivityController(AccountBase accountBase, IDataService dataService)
        {
            this.AccountBase = accountBase;
            this.DataService = dataService;
        }

        //
        // GET: /Default1/

        public ViewResult Index()
        {
            ActivityDiscoveryModel adm = new ActivityDiscoveryModel(this.AccountBase);
            adm.Setup();
            return View(adm);
        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(int id)
        {
            var o = DataService.Activity.Find(id);
            Permissions p = this.AccountBase.GetPermissions(o);
            if (!p.HasFlag(Permissions.View))
            {
                return RedirectToAction("Index");
            }
            return View(o);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Default1/Create
        [HttpPost]
        public ActionResult Create(Activity activity)
        {
            if (ModelState.IsValid)
            {
                activity.Organizer = this.AccountBase.GetAccount();

                return RedirectToAction("AddFacebookFriends", new { id = activity.Id });
            }
            return View(activity);
        }

        public ActionResult CreateFromEvent(int eventId)
        {
            var ev = AccountBase.EventActions.GetEvent(eventId);
            var m = new ActivityCreationModel(null, ev);
            return View(m);
        }

        [HttpPost]
        public ActionResult CreateFromEvent(int eventId, Privacy privacy, string Invited)
        {
            var ev = AccountBase.EventActions.GetEvent(eventId);
            var actv = AccountBase.ActivityActions.CreateActivityFromExistingEvent(ev, privacy);
            return RedirectToAction("Index", "Activity");
        }

        //
        // GET: /Default1/Edit/5 
        public ActionResult Edit(int id)
        {
            var e = DataService.Activity.Find(id);
            if (e != null)
            {
                return View(e);
            }
            else
            {
                // TODO: what to do when there is no such event
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(Activity activity)
        {
            return RedirectToAction("AddFacebookFriends", new { id = activity.Id });
        }

        public ActionResult AcceptInvitation(int id)
        {
            var e = this.DataService.Activity.Find(id);
            if (e != null)
            {
                return View(e);
            }
            else
            {
                // TODO: what to do when there is no such event
                return RedirectToAction("Index");
            }
        }

        //
        // GET: /Default1/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            return View();
        }


        [ActionName("AddFacebookFriends")]
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult AddFacebookFriends(int id)
        {
            var e = DataService.Activity.Find(id);
            if (e != null)
            {
                return View(e);
            }
            else
            {
                // TODO: what to do when there is no such event
                return RedirectToAction("Index");
            }
        }
    }
}