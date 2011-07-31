using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Facebook.Web.Mvc;
using PraLoup.BusinessLogic;
using PraLoup.Utilities;
using PraLoup.WebApp.Areas.Admin.Models;
using PraLoup.WebApp.Utilities;
using BusinessModels = PraLoup.WebApp.Areas.Business.Models;
using DataEntites = PraLoup.DataAccess.Entities;
using ModelEntities = PraLoup.WebApp.Models.Entities;

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
                var pm = new PromoModel(this.AccountBase, promotionId.Value);
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
        public ActionResult Create(Guid? businessId)
        {
            if (businessId.HasValue)
            {
                var pcm = new PromoCreateModel(this.AccountBase, businessId.Value, new ModelEntities.Promotion());
                pcm.Setup();
                return View(pcm);
            }
            else
            {
                return View("Error");
            }
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
        public ActionResult Edit(Guid id, PromoCreateModel pcm)
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
            var pcm = new PromoCreateModel();
            // pcm.Promotion.Deals.AddUserGroups(new List<UserGroup>());
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

            var pcm = new BusinessModels.PromotionCloseModel();
            pcm.Promotion = AccountBase.PromotionActions.GetPromotion(promoid);
            var invites = AccountBase.PromotionInstanceActions.GetUserInvitesForPromotion(pcm.Promotion);
            pcm.UserCloseData = new List<BusinessModels.UserCloseData>();
            foreach (var invite in invites)
            {
                var ucd = new BusinessModels.UserCloseData();
                ucd.Invite = invite;
                ucd.Rating = new BusinessModels.StarModel();
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


        private static ModelEntities.Deal ConvertDynamicToDeal(dynamic deal)
        {
            var d = new ModelEntities.Deal();
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
        public ActionResult PromotionCreate(PromoCreateModel pcm)
        {
            this.AccountBase.SetupActionAccount();

            // Because of the weird control, we fetch deals from a json param:
            string jsonDeal = Request.Form["Deals"];
            // first try to serialize it as a single thing, then try as an array;
            dynamic deal = null;
            pcm.Promotion.Deals = new List<ModelEntities.Deal>();
            try
            {
                deal = jsonDeal.GetJson();
                var d = ConvertDynamicToDeal(deal);
                pcm.Promotion.Deals.Add(d);
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
                        var d = ConvertDynamicToDeal(foo);
                        pcm.Promotion.Deals.Add(d);
                    }
                }
                else
                {
                    //error
                }
            }

            var p = AutoMapper.Mapper.Map<ModelEntities.Promotion, DataEntites.Promotion>(pcm.Promotion);

            List<PraLoup.DataAccess.Entities.Business> userbusinesses = new List<PraLoup.DataAccess.Entities.Business>();
            var userBusiness = AccountBase.BusinessActions.GetBusinessForUser(AccountBase.Account).FirstOrDefault();
            //TODO: we should throw exception here. this is error condition, the user should not create promotion without business
            p.Business = userbusinesses != null ? userBusiness : new DataEntites.Business();
            this.AccountBase.PromotionActions.SavePromotion(p);
            return View(pcm);
        }
    }
}
