using System;
using System.Web.Mvc;
using PraLoup.BusinessLogic;
using PraLoup.DataAccess.Entities;
using PraLoup.WebApp.Areas.Admin.Models;
using PraLoup.WebApp.Utilities;
namespace PraLoup.WebApp.Areas.Admin.Controllers
{
    public class PromotionController : Controller
    {
        private AccountBase AccountBase;
        public PromotionController(AccountBase account)
        {
            this.AccountBase = account;
        }

        //
        // GET: /Business/Promotion/
        // business can only look at the promotion for its own business
        public ActionResult Index(Guid businessId, string businessName)
        {
            this.AccountBase.SetupActionAccount();
            var pim = new PromoIndexModel(this.AccountBase, businessId, businessName);
            return View(pim);
        }

        //
        // GET: /Business/Promotion/Details/5

        public ActionResult Details(Guid promotionId)
        {
            this.AccountBase.SetupActionAccount();
            var pm = new PromoModel(this.AccountBase, promotionId);
            return View(pm);
        }

        //
        // GET: /Business/Promotion/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Business/Promotion/Create

        [HttpPost]
        [UnitOfWork]
        public ActionResult Create(Promotion p)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Business/Promotion/Edit/5
        public ActionResult Edit(Guid promoId)
        {
            return View();
        }

        //
        // POST: /Business/Promotion/Edit/5

        [HttpPost]
        [UnitOfWork]
        public ActionResult Edit(Guid id, Promotion p)
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
        // GET: /Business/Promotion/Edit/5
        public ActionResult CreatePromoInstance(Guid id)
        {
            return View();
        }
    }
}
