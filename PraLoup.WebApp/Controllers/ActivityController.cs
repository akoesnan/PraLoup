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
        public ActionResult CreateFromEvent(int eventId, Privacy privacy)
        {
            var ev = AccountBase.EventActions.GetEvent(eventId);
            var actv = AccountBase.ActivityActions.CreateActivityFromExistingEvent(ev, privacy);
            return View("AddFacebookFriends", actv);
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

            var o = this.DataService.Activity.Find(id);
            //both the uncondensed and condensed fb:form-request will contain a key called "id[]" which will contain a list of facebook id's
            if (results.Any(s => s.Key == "ids[]"))
            {
                string facebookUserIds = results.SingleOrDefault(s => s.Key == "ids[]").Value;
                tokens = facebookUserIds.Split(',');
                List<Account> fas = new List<Account>();
                foreach (string token in tokens)
                {
                    FacebookAccount fa = new FacebookAccount(token);
                    fas.Add(fa.Account);
                }

                var invitations = fas.Select(a => new Invitation(this.AccountBase.GetAccount(), a, o, "invite message"));
                DataService.Commit();
                found = true;
            }

            // return RedirectToAction("Index", "Friends", new { invitationsSent = true });
            if (found)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var e = DataService.Activity.Find(id);
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