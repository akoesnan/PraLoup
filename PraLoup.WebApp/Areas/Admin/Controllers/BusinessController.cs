using System;
using System.Web.Mvc;
using Facebook.Web.Mvc;
using PraLoup.BusinessLogic;
using PraLoup.Infrastructure.Logging;
using PraLoup.WebApp.Areas.Admin.Models;
using PraLoup.WebApp.Utilities;
using DataEntities = PraLoup.DataAccess.Entities;
using ModelEntities = PraLoup.WebApp.Models.Entities;

namespace PraLoup.WebApp.Areas.Admin.Controllers
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
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
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
            var bm = new BusinessModel(this.AccountBase, id);
            return View(bm);
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
                        return RedirectToAction("Index");
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
            var bm = new BusinessModel(this.AccountBase, id);
            return View(bm);
        }

        //
        // POST: /Business/Edit/5

        [HttpPost]
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        [UnitOfWork]
        public ActionResult Edit(Guid id, BusinessModel businessModel)
        {
            this.AccountBase.SetupActionAccount();
            try
            {
                if (ModelState.IsValid)
                {
                    var b = AutoMapper.Mapper.Map<ModelEntities.Business, DataEntities.Business>(businessModel.Business);
                    this.AccountBase.BusinessActions.SaveOrUpdateBusiness(b);

                    return RedirectToAction("Index");
                }
                return View(businessModel);
            }
            catch
            {
                return View();
            }
        }
    }
}
