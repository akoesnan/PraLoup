using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Facebook.Web.Mvc;
using PraLoup.BusinessLogic;
using PraLoup.WebApp.Areas.Business.Models;
using PraLoup.Utilities;
using PraLoup.WebApp.Utilities;
using Entities = PraLoup.WebApp.Models.Entities;


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
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
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
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
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
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
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
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult Edit(Guid promoId)
        {
            return View();
        }

        //
        // POST: /Business/Promotion/Edit/5

        [HttpPost]
        [UnitOfWork]
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
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
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult CreatePromoInstance(Guid id)
        {
            return View();
        }


        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult PromotionCreate()
        {
            PromotionCreateModel pcm = new PromotionCreateModel();
            pcm.Deals.AddUserGroups(new List<UserGroup>());
            return View(pcm);
        }

        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
        public ActionResult Close(Guid promoid)
        {
            AccountBase.SetupActionAccount();
            var busy = AccountBase.BusinessActions.GetBusinessForUser(this.AccountBase.Account);
            List<PraLoup.DataAccess.Entities.Business> busies = new List<DataAccess.Entities.Business>(busy);
            //
            // we should have some ui and state that tells which business the user is working on.
            //
            PraLoup.DataAccess.Entities.Business business = busies[0];

            PromotionCloseModel pcm = new PromotionCloseModel();
            pcm.Promotion = AccountBase.PromotionActions.GetPromotion(promoid);
            var invites = AccountBase.PromotionInstanceActions.GetUserInvitesForPromotion(pcm.Promotion);
            pcm.UserCloseData = new List<UserCloseData>();
            foreach (var invite in invites)
            {
                UserCloseData ucd = new UserCloseData();
                ucd.Invite = invite;
                ucd.Rating = new StarModel();
                ucd.Rating.Max = 10;
                if (ucd.Invite.UserRatings.Count > 0)
                {
                    ucd.Rating.Value = ucd.Invite.UserRatings[0].Rate;
                    ucd.AmountSpent = ucd.Invite.UserRatings[0].AmountSpent.Value;
                }
                ucd.Redeemed = ucd.Invite.IsAttended;
                ucd.UserGroups = AccountBase.UserGroupActions.GetUserGroupsForBusinessAndUser(ucd.Invite.Recipient, business);
                pcm.UserCloseData.Add(ucd);
            }

            return View(pcm);
        }


        private static Entities.Deal ConvertDynamicToDeal(dynamic deal)
        {
            Entities.Deal d = new Entities.Deal();
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
        [FacebookAuthorize(LoginUrl = "/PraLoup.WebApp/Account/Login")]
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
                Entities.Deal d = ConvertDynamicToDeal(deal);
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

            Entities.Promotion p = pcm.ToPromotion();
            List<PraLoup.DataAccess.Entities.Business> userbusinesses = new List<PraLoup.DataAccess.Entities.Business>(AccountBase.BusinessActions.GetBusinessForUser(AccountBase.Account));
            p.Business = userbusinesses != null && userbusinesses.Count > 0 ? userbusinesses[0] : new PraLoup.DataAccess.Entities.Business();
            this.AccountBase.PromotionActions.SavePromotion(p);
            return View(pcm);
        }
    }
}
