using System;
using System.Web.Mvc;
using Facebook.Web.Mvc;
using PraLoup.BusinessLogic;
using PraLoup.Infrastructure.Logging;
using PraLoup.WebApp.Areas.Admin.Models;
using PraLoup.WebApp.Resources;
using PraLoup.WebApp.Utilities;

namespace PraLoup.WebApp.Areas.Business.Controllers
{
    public class BusinessController : Controller
    {
        private AccountBase AccountBase;
        private ILogger Log;

        public BusinessController(AccountBase accountBase, ILogger logger)
        {
            this.AccountBase = accountBase;
            this.Log = logger;
        }

        //
        // GET: /Business/

        public ActionResult Index()
        {
            var bim = new BusinessIndexModel(this.AccountBase, 10, 0);
            return View(bim);
        }

        //
        // GET: /Business/Details/5
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult Details(Guid businessId, String businessName, String message)
        {
            BusinessModel businessModel = new BusinessModel(this.AccountBase, businessId);
            if (businessModel.IsValid)
            {
                this.AccountBase.SetupActionAccount();
                return View(businessModel);
            }
            else
            {
                this.AccountBase.SetupActionAccount();
                return View();
            }
        }

        //
        // GET: /Business/Create        
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult Create()
        {
            this.AccountBase.SetupActionAccount();
            return View(new BusinessCreateEditModel());
        }

        //
        // POST: /Business/Create
        [HttpPost]
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        [UnitOfWork]
        public ActionResult Create(BusinessCreateEditModel bcm)
        {
            this.AccountBase.SetupActionAccount();
            try
            {
                if (ModelState.IsValid)
                {
                    bcm.AccountBase = this.AccountBase;
                    if (bcm.SaveOrUpdate())
                    {
                        return RedirectToAction("Details", "Business",
                            new { businessId = bcm.Business.Id, message = LocStrings.BusinessCreated });
                    }
                    else
                    {
                        return View(bcm);
                    }
                }
                else
                {
                    return View(bcm);
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Business/Edit/5 
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult Edit(Guid id)
        {
            this.AccountBase.SetupActionAccount();
            BusinessModel businessModel = new BusinessModel(this.AccountBase, id);
            return View(businessModel);
        }

        //
        // POST: /Business/Edit/5

        [HttpPost]
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        [UnitOfWork]
        public ActionResult Edit(BusinessCreateEditModel bcm)
        {
            this.AccountBase.SetupActionAccount();
            try
            {
                if (ModelState.IsValid)
                {
                    bcm.AccountBase = this.AccountBase;
                    if (bcm.SaveOrUpdate())
                    {
                        return RedirectToAction("Details", "Business",
                            new { businessId = bcm.Business.Id, message = LocStrings.BusinessCreated });
                    }
                    else
                    {
                        return View(bcm);
                    }
                }
                else
                {
                    return View(bcm);
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Business/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Business/Delete/5
        [HttpPost]
        [UnitOfWork]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }
}
