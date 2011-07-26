﻿using System.Web.Mvc;
using Facebook.Web.Mvc;
using PraLoup.BusinessLogic;
using PraLoup.DataAccess.Entities;
using PraLoup.Infrastructure.Logging;
using PraLoup.WebApp.Models;
using System.Linq;
using System.Web.Script.Serialization;
using PraLoup.Utilities;

namespace PraLoup.WebApp.Controllers
{
    public class PromotionController : Controller
    {
        
        public AccountBase AccountBase { get; private set; }

        public PromotionController(AccountBase accountBase, ILogger logger)
        {
            this.AccountBase = accountBase;
        }

        public ActionResult Forward(int promotionid, string ids, string message)
        {
            this.AccountBase.SetupActionAccount();
            PromotionInstance pi = null;

            bool isForUser = this.AccountBase.PromotionInstanceActions.PromotionInstanceForUserPromotion(promotionid, out pi);

            this.AccountBase.PromotionInstanceActions.Forward(pi, null, message); 

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
            d.Available = deal.DealListAvailable;
            d.OriginalValue = deal.DealListOriginalValue;
            d.DealValue = deal.DealListCurrentValue;
            d.Saving = deal.DealListSaving;
            d.StartDateTime = deal.DealListStartDateTime;
            d.EndDateTime = deal.DealListEndDateTime;
            d.Description = deal.DealListDescription;
            d.FinePrint = deal.DealListFinePrint;
            d.RedemptionInstructions = deal.DealListRedemptionInstructions;
            return d;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PromotionCreate(PromotionCreateModel pcm)
        {
            // Because of the weird control, we fetch deals from a json param:
            string jsonDeal = Request.Form["Deals"];
            // first try to serialize it as a single thing, then try as an array;
            dynamic deal = jsonDeal.GetJson();
            pcm.Deals = new DealList();
            if (deal != null)
            {
                Deal d = ConvertDynamicToDeal(deal);
                pcm.Deals.Add(d);
            }
            else
            {
                jsonDeal = "[" + jsonDeal + "]";
                deal = jsonDeal.GetJson();
                if (deal != null)
                {
                    foreach (dynamic foo in deal)
                    {
                        Deal d = ConvertDynamicToDeal(deal);
                        pcm.Deals.Add(d);
                    }
                }
                else
                {
                    //error
                }
            }

            return View(pcm);
        }

        public ActionResult View(int promotionid)
        {
            this.AccountBase.SetupActionAccount();
            PromotionInstance pi = null;

            bool isForUser = this.AccountBase.PromotionInstanceActions.PromotionInstanceForUserPromotion(promotionid, out pi);
            // the promotion id might not be for the user. In that case, we'll see if the user has other promotions for that event
            if (isForUser)
            {
                // show model and return
                PromotionViewModel pvm = new PromotionViewModel();
                pvm.Deal = pi.Deal;
                pvm.Event = pi.Promotion.Event;
                var invited = from t in pi.ForwardedPromotionInstances select t.Recipient;
                pvm.Invited = new AccountListJson(invited);
                pvm.InviteDate = pi.CreateDateTime;
                pvm.Promotion = pi.Promotion;
                pvm.Sender = pi.Sender;
                pvm.Status = pi.Status.StatusType;
                pvm.Message = pi.Message;
                return View();
            }

            if (pi == null)
            {
                var otherInvites = this.AccountBase.PromotionInstanceActions.GetAvailableInvitationsForUser(pi.Promotion.Event.Id);
                if (otherInvites != null)
                {
                    //redirect to the page for the first invite
                    return RedirectToAction("View", "PromotionController", new { id = otherInvites[0].Id });
                }
                
                if(pi.Promotion.Event.Permission.CanView)
                {
                    // redirect to event view 
                    return RedirectToAction("Details", "EventController", new { id = pi.Promotion.Event.Id });
                }
            }

            // show the error page
            return RedirectToAction("Error", "Home");
        }
    }
}
