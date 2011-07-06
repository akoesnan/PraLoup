using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Facebook.Web.Mvc;
using PraLoup.BusinessLogic;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Entities;
using PraLoup.FacebookObjects;
using PraLoup.WebApp.Models;

namespace PraLoup.WebApp.Controllers
{ 
    public class ActivityController : Controller
    {
        GenericRepository db = new GenericRepository(new EntityRepository());

        //
        // GET: /Default1/

        public ViewResult Index()
        {
            var entities = db.GetAll<Activity>();
            List<ActivityModel> ams = new List<ActivityModel>();
            foreach (var am in entities)
            {
                ActivityModel em = new ActivityModel();
                em.Permissions = FacebookAccount.Current.GetPermissions(am);
                em.Activity = am;
                ams.Add(em);
            }
            return View(ams);
        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(int id)
        {

            var o = db.Find<Activity>(id);
            Permissions p = FacebookAccount.Current.GetPermissions(o);
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
                activity.Organizer = FacebookAccount.Current.GetAccount();
                db.Add(activity);
                db.SaveChanges();
                return RedirectToAction("AddFacebookFriends", new { id = activity.ActivityId });
            }

            return View(activity);
        }

        public ActionResult CreateFromEvent(int id)
        {
            // create the activity and save it, that way we can fetch this stuff again later.
            Activity f = new Activity();
            f.Event = db.Find<Event>(id);
            f.Organizer = FacebookAccount.Current.GetAccount();
            f.IsCreated = false;
            db.Add(f);
            db.SaveChanges();
            
            
            return View(f);
        }

        [HttpPost]
        public ActionResult CreateFromEvent(Activity am)
        {
            if (ModelState.IsValid)
            {
                //fetch item from db, fix updated fields
                Activity dba = db.Find<Activity>(am.ActivityId);
                dba.Privacy = am.Privacy;
                dba.IsCreated = true;
                db.SaveChanges();
                return RedirectToAction("AddFacebookFriends", new { id = am.ActivityId });
            }

            return View(am);
        }
        //
        // GET: /Default1/Edit/5 
        public ActionResult Edit(int id)
        {
            var e = db.Find<Activity>(id);
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
            return RedirectToAction("AddFacebookFriends", new { id = activity.ActivityId });
        }

        public ActionResult AcceptInvitation(int id)
        {
            var e = db.Find<Activity>(id);
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [ActionName("AddFacebookFriends")]
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult AddFacebookFriends(int id)
        {
            var e = db.Find<Activity>(id);
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

        [ActionName("AddFacebookFriends")]
        [AcceptVerbs(HttpVerbs.Post)]
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult AddFacebookFriends(int id, FormCollection formCollection)
        {

            //from the form collection, create a key value pair (just easier to work with for me, you dont have to do this)
            List<KeyValuePair<string, string>> results = new List<KeyValuePair<string, string>>();
            foreach (string key in formCollection.AllKeys)
            {
                results.Add(new KeyValuePair<string, string>(key, formCollection[key]));
            }

            string[] tokens;
            bool found = false;
            OAuthHandler oauth = new OAuthHandler();
            /*
            //in the uncondensed fb:form-request, there will be a form collection key called "emails[]" which will contain a comma delimited list of emails
            if (results.Any(s => s.Key == "emails[]"))
            {
                string emails = results.SingleOrDefault(s => s.Key == "emails[]").Value;
                tokens = emails.Split(',');
                foreach (string token in tokens)
                {
                    
                }
                found = true;
            }
            */

            var o = db.Find<Activity>(id);
            //both the uncondensed and condensed fb:form-request will contain a key called "id[]" which will contain a list of facebook id's
            if (results.Any(s => s.Key == "ids[]"))
            {
                string facebookUserIds = results.SingleOrDefault(s => s.Key == "ids[]").Value;
                tokens = facebookUserIds.Split(',');
                List<Account> fas = new List<Account>();
                foreach (string token in tokens)
                {
                    FacebookAccount fa = new FacebookAccount(token);
                    fas.Add(fa.GetAccount());
                }
                Invitation i = new Invitation();
                i.Activity = o;
                i.CreateDateTime = System.DateTime.Now;

                i.Recipients = new Accounts(fas);
                found = true;
            }

            // return RedirectToAction("Index", "Friends", new { invitationsSent = true });
            if (found)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var e = db.Find<Event>(id);
                if (e != null)
                {
                    return View(e);
                }
                // todo handle error
                return RedirectToAction("Index");
            }
        }
    }
}