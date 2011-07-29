using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PraLoup.BusinessLogic;
using PraLoup.DataAccess.Entities;
using PraLoup.Utilities;
using PraLoup.WebApp.Areas.Business.Models;
using PraLoup.WebApp.Utilities;


namespace PraLoup.WebApp.Areas.Business.Controllers
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



        public ActionResult PromotionCreate()
        {
            PromotionCreateModel pcm = new PromotionCreateModel();
            return View(pcm);
        }

        private static Deal ConvertDynamicToDeal(dynamic deal)
        {
            Deal d = new Deal();
            d.Available = int.Parse(deal.DealListAvailable);
            d.OriginalValue = int.Parse(deal.DealListOriginalValue);
            d.DealValue = int.Parse(deal.DealListCurrentValue);
            d.Saving = int.Parse(deal.DealListSaving);
            d.StartDateTime = DateTime.Parse(deal.DealListStartDateTime);
            d.EndDateTime = DateTime.Parse(deal.DealListEndDateTime);
            d.Description = deal.DealListDescription;
            d.FinePrint = deal.DealListFinePrint;
            d.RedemptionInstructions = deal.DealListRedemptionInstructions;
            return d;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [UnitOfWork]
        public ActionResult PromotionCreate(PromotionCreateModel pcm)
        {
            this.AccountBase.SetupActionAccount();

            // Because of the weird control, we fetch deals from a json param:
            string jsonDeal = Request.Form["Deals"];
            // first try to serialize it as a single thing, then try as an array;
            dynamic deal = null;
            pcm.Deals = new DealList();
            try
            {
                deal = jsonDeal.GetJson();
                Deal d = ConvertDynamicToDeal(deal);
                pcm.Deals.Add(d);
            }
            catch (Exception)
            {
            }
            if (deal == null)
            {
                jsonDeal = "[" + jsonDeal + "]";
                deal = jsonDeal.GetJson();
                if (deal != null)
                {
                    foreach (dynamic foo in deal)
                    {
                        Deal d = ConvertDynamicToDeal(foo);
                        pcm.Deals.Add(d);
                    }
                }
                else
                {
                    //error
                }
            }

            Promotion p = pcm.ToPromotion();
            List<PraLoup.DataAccess.Entities.Business> userbusinesses = new List<PraLoup.DataAccess.Entities.Business>(AccountBase.BusinessActions.GetBusinessForUser(AccountBase.Account));
            p.Business = userbusinesses != null && userbusinesses.Count > 0 ? userbusinesses[0] : new PraLoup.DataAccess.Entities.Business();
            this.AccountBase.PromotionActions.SavePromotion(p);
            return View(pcm);
        }
    }
}
