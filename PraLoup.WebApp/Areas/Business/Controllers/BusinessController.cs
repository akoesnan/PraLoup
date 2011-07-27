using System;
using System.Web.Mvc;
using Facebook.Web.Mvc;
using PraLoup.BusinessLogic;
using PraLoup.DataAccess.Enums;
using PraLoup.Infrastructure.Logging;
using PraLoup.WebApp.Areas.Admin.Models;
using PraLoup.WebApp.Utilities;
using Entities = PraLoup.DataAccess.Entities;

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
        public ActionResult Details(Guid id, String businessName)
        {
            this.AccountBase.SetupActionAccount();
            return View();
        }

        //
        // GET: /Business/Create        
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult Create()
        {
            this.AccountBase.SetupActionAccount();
            return View();
        }

        //
        // POST: /Business/Create
        [HttpPost]
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        [UnitOfWork]
        public ActionResult Create(Entities.Business b, Role role)
        {
            this.AccountBase.SetupActionAccount();
            try
            {
                if (ModelState.IsValid)
                {
                    var bcm = new BusinessCreateModel(this.AccountBase, new Entities.Business(), Role.BusinessAdmin);
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
        // GET: /Business/Edit/5 
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Business/Edit/5

        [HttpPost]
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        [UnitOfWork]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
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
