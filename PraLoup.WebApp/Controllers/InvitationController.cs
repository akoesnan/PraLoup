﻿using System;
using System.Linq;
using System.Web.Mvc;
using PraLoup.BusinessLogic;
using PraLoup.DataAccess.Entities;
using PraLoup.Infrastructure.Logging;
using PraLoup.WebApp.Models;

namespace PraLoup.WebApp.Controllers
{
    public class InvitationController : Controller
    {
        public AccountBase AccountBase { get; private set; }

        public InvitationController(AccountBase accountBase, ILogger logger)
        {
            this.AccountBase = accountBase;
        }

        public ActionResult Forward(Guid promotionid, string ids, string message)
        {
            this.AccountBase.SetupActionAccount();
            PromotionInstance pi = null;

            bool isForUser = this.AccountBase.PromotionInstanceActions.PromotionInstanceForUserPromotion(promotionid, out pi);

            this.AccountBase.PromotionInstanceActions.Forward(pi, null, message); 

            return View();
        }

        public ActionResult View(Guid promotionid)
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
