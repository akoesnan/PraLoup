using System;
using System.Collections.Generic;
using System.Linq;
using PraLoup.BusinessLogic.Plugins;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;
using PraLoup.DataAccess.Services;
using PraLoup.Infrastructure.Logging;

namespace PraLoup.BusinessLogic
{
    public class PromotionInstanceActions : ActionBase<IPromotionInstanceAction>
    {
        public PromotionInstanceActions(Account account, IDataService dataService, ILogger log, IEnumerable<IPromotionInstanceAction> inviteActionPlugins)
            : base(account, dataService, log, inviteActionPlugins)
        {
        }

        public IEnumerable<PromotionInstance> CreatePromoInstance(Promotion promotion, IEnumerable<Account> invites, string message)
        {
            if (promotion == null)
            {
                throw new ArgumentException("promotion should not be null");
            }

            if (invites == null || invites.Count() == 0)
            {
                throw new ArgumentException("there should be one or more invites");
            }

            var promoInstances = from i in invites
                                 select new PromotionInstance(this.Account, i, promotion, promotion.Deals.FirstOrDefault(), message);

            IEnumerable<string> brokenRules;
            var success = this.dataService.PromotionInstance.SaveOrUpdateAll(promoInstances, out brokenRules);
            if (success)
            {
                this.log.Info("Sucessfully {0} promotion instance for {1}", promoInstances.Count(), promotion);
                ExecutePlugins(f => f.CreatePromoInstance(promotion, invites, message));                
                return promoInstances;
            }
            else
            {
                this.log.Debug("Error: Unable to save invitations broken rules {0}", String.Join(",", brokenRules));
                return null;
            }
        }

        public IEnumerable<PromotionInstance> Forward(PromotionInstance pi, IEnumerable<Account> invites, string message)
        {
            if (pi == null)
            {
                throw new ArgumentException("promotion should not be null");
            }

            if (invites == null || invites.Count() == 0)
            {
                throw new ArgumentException("there should be one or more invites");
            }

            // TODO: what is the logic to decide what is the deal that can be forwarded
            var forwards = from i in invites
                           select new PromotionInstance(this.Account, i, pi.Promotion, pi.Deal, message);

            IEnumerable<string> brokenRules;
            var success = this.dataService.PromotionInstance.SaveOrUpdateAll(forwards, out brokenRules);
            if (success)
            {
                this.log.Info("Sucessfully saving {0} promotion forwards for {1}", forwards.Count(), pi.Promotion);
                ExecutePlugins(f => f.Forward(pi, invites, message));                
                return forwards;
            }
            else
            {
                this.log.Debug("Error: Unable to save invitations broken rules {0}", String.Join(",", brokenRules));
                return null;
            }
        }

        public PromotionInstance Accept(PromotionInstance pi, string message)
        {
            if (this.Account != pi.Recipient)
            {
                return null;
            }
            else
            {
                var c = GenerateCoupon(pi);
                pi.Coupons = pi.Coupons ?? new List<Coupon>();
                pi.Coupons.Add(c);

                return Response(pi, StatusType.Accept, message);
            }
        }

        public PromotionInstance Decline(PromotionInstance pi, string message)
        {
            if (this.Account != pi.Recipient)
            {
                return null;
            }
            else
            {
                return Response(pi, StatusType.Decline, message);
            }
        }

        public PromotionInstance Maybe(PromotionInstance pi, string message)
        {
            if (this.Account != pi.Recipient)
            {
                return null;
            }
            else
            {
                var c = GenerateCoupon(pi);
                pi.Coupons = pi.Coupons ?? new List<Coupon>();
                pi.Coupons.Add(c);

                return Response(pi, StatusType.Maybe, message);
            }
        }

        public PromotionInstance Response(PromotionInstance pi, StatusType responseType, string message)
        {
            if (pi.Recipient.Id != this.Account.Id)
            {
                throw new ArgumentException(String.Format("this invitation is not for user {0}", this.Account));
            }

            pi.Status.StatusType = responseType;
            pi.Status.Message = message;
            pi.CreateDateTime = DateTime.UtcNow;

            IEnumerable<string> brokenRules;
            var success = this.dataService.PromotionInstance.SaveOrUpdate(pi, out brokenRules);

            if (success)
            {                
                this.log.Info("[] - {0} Succesfully saved invitation {1}", Account, pi);
                // TODO: this should not be here, we should decouple facebook stuff
                ExecutePlugins(t => t.Response(pi));
                return pi;
            }
            else
            {                
                this.log.Debug("[] - {0} Unable to create activity {1}. Violated rules {2}", Account, pi, brokenRules.First());
                return null;
            }
        }

        private Coupon GenerateCoupon(PromotionInstance pi)
        {
            if (pi.Deal != null)
            {
                var coupon = new Coupon();
                coupon.CouponCode = coupon.GenerateCode(8);
                coupon.Redeemed = false;
                return coupon;
            }
            return null;
        }


        public bool PromotionInstanceForUserPromotion(int promotionInstanceId, out PromotionInstance pi)
        {
            pi = null;
            // see if we can find the instance
            var x = this.dataService.PromotionInstance.Find(promotionInstanceId);
            if (x != null)
            {
                pi = x;
                if (x.Recipient.FacebookLogon.FacebookId == this.Account.FacebookLogon.FacebookId)
                {
                    return true;
                }
            }
            return false;
        }

        public IList<PromotionInstance> GetAvailableInvitationsForUser()
        {
            // see if we can find the instance
            var y = this.dataService.PromotionInstance.Where(x => x.Recipient.FacebookLogon.FacebookId == this.Account.FacebookLogon.FacebookId);

            if (y != null)
            {
                return y.ToList<PromotionInstance>();
            }
            return null;
        }


        /// <summary>
        /// Right now, this only returns the first created invitation, should be smarter using 
        /// the business' rules in the future.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public IList<PromotionInstance> GetAvailableInvitationsForUser(Guid eventId)
        {
            // see if we can find the instance
            var y = this.dataService.PromotionInstance.Where(x => x.Recipient.FacebookLogon.FacebookId == this.Account.FacebookLogon.FacebookId
                                                                && x.Promotion.Event.Id == eventId).OrderBy(x => x.CreateDateTime);

            if (y != null)
            {
                return y.ToList<PromotionInstance>();
            }
            return null;
        }

        public IList<PromotionInstance> GetUserInvitesForPromotion(Promotion promo)
        {
            var y = this.dataService.PromotionInstance.Where(x => x.Promotion.Id == promo.Id);
            if (y != null)
            {
                return y.ToList<PromotionInstance>();
            }
            return null;
        }
    }
}
