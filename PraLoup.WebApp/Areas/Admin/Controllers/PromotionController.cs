using System;
using System.Web.Mvc;
using PraLoup.BusinessLogic;
using PraLoup.WebApp.Areas.Admin.Models;
using PraLoup.WebApp.Utilities;
using Entities = PraLoup.WebApp.Models.Entities;

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
        public ActionResult Index(Guid? businessId, string businessName)
        {
            if (businessId.HasValue || !string.IsNullOrEmpty(businessName))
            {
                this.AccountBase.SetupActionAccount();
                var pim = new PromoIndexModel(this.AccountBase, businessId, businessName);
                return View(pim);
            }
            return View("Error");
        }

        //
        // GET: /Business/Promotion/Details/5

        public ActionResult Details(Guid? promotionId)
        {
            if (promotionId.HasValue)
            {
                this.AccountBase.SetupActionAccount();
                var pm = new PromoModel(this.AccountBase, promotionId);
                return View(pm);
            }
            else
            {
                return View("Error");
            }
        }

        //
        // GET: /Business/Promotion/Create
        public ActionResult Create(Guid businessId)
        {
            var pcm = new PromoCreateModel(this.AccountBase, businessId, new Entities.Promotion());
            pcm.Setup();
            return View(pcm);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [UnitOfWork]
        public ActionResult Create(PromoCreateModel pcm)
        {
            this.AccountBase.SetupActionAccount();

            //if (ModelState.IsValid)
            //{
            //try
            //{
            pcm.AccountBase = this.AccountBase;
            var success = pcm.CreatePromotion();
            if (success)
            {
                return View("Index");
            }
            else
            {
                return View(pcm);
            }
            //}
            //catch
            //{
            //    return View(pcm);
            //}
            //}
            //else
            //{
            //    return View(pcm);
            //}
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
        public ActionResult Edit(Guid id, Entities.Promotion p)
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
