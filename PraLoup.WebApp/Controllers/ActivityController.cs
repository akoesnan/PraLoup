using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Facebook.Web.Mvc;
using PraLoup.BusinessLogic;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Interfaces;
using PraLoup.Facebook;
using PraLoup.Plugins;

namespace PraLoup.WebApp.Controllers
{ 
    public class ActivityController : Controller
    {
        private IRepository Repository {get;set;}
        private IEnumerable<IEventAction> EventActionPlugins { get; set; }

        private AccountBase _accountbase;

        private AccountBase AccountBase
        {
            get 
            {
                if (_accountbase == null) {
                    _accountbase = new AccountBase(this.Repository, this.EventActionPlugins);
                }
                return _accountbase;
            }
        }

        public ActivityController(IRepository repository, IEnumerable<IEventAction> eventActionPlugins) {
            this.Repository = repository;
            this.EventActionPlugins = eventActionPlugins;
        }

        //
        // GET: /Default1/

        public ViewResult Index()
        {
            var entities = Repository.GetAll<Activity>();
            return View(entities);
        }

        //
        // GET: /Default1/Details/5

        public ViewResult Details(int id)
        {

            var o = Repository.Find<Activity>(id);
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
                Repository.Add(activity);
                Repository.SaveChanges();
                return RedirectToAction("AddFacebookFriends", new { id = activity.Id });
            }

            return View(activity);
        }
        
        //
        // GET: /Default1/Edit/5 
        public ActionResult Edit(int id)
        {
            var e = Repository.Find<Activity>(id);
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
            Repository.Dispose();
            base.Dispose(disposing);
        }

        [ActionName("AddFacebookFriends")]
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult AddFacebookFriends(int id)
        {
            var e = Repository.Find<Activity>(id);
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
             var o = Repository.Find<Activity>(id);
            //both the uncondensed and condensed fb:form-request will contain a key called "id[]" which will contain a list of facebook id's
            if (results.Any(s => s.Key == "ids[]"))
            {
                string facebookUserIds = results.SingleOrDefault(s => s.Key == "ids[]").Value;
                tokens = facebookUserIds.Split(',');
                List<Account> fas = new List<Account>();
                foreach (string token in tokens)
                {
                    // Are we registering their friends as well?
                    //FacebookAccount fa = new FacebookAccount(this.Repository, oauth, token);
                    //if (!fa.IsCreated())
                    //{
                    //    fa.Register();
                    //}

                    //fas.Add(fa.GetAccount());
                }

                var invitations = fas.Select(a => new Invitation()
                {
                    Activity = o,
                    CreateDateTime = System.DateTime.UtcNow,
                    Recipient = a
                });
                found = true;                 
            }

            // return RedirectToAction("Index", "Friends", new { invitationsSent = true });
            if (found)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var e = Repository.Find<Event>(id);
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