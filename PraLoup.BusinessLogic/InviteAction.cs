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
    public class PromotionInstanceAction : ActionBase<IPromotionInstanceAction>
    {
        public PromotionInstanceAction(IDataService dataService, IEnumerable<IPromotionInstanceAction> inviteActionPlugins, ILogger log)
            : base(dataService, log, inviteActionPlugins)
        {
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
            var success = this.dataService.Invitation.SaveOrUpdateAll(forwards, out brokenRules);
            if (success)
            {
                this.log.Info("Sucessfully saving {0} promotion forwards for {1}", forwards.Count(), pi.Promotion);
                ExecutePlugins(f => f.Forward(pi, invites, message));
                this.dataService.Commit();
                return forwards;
            }
            else
            {
                this.log.Debug("Error: Unable to save invitations broken rules {0}", String.Join(",", brokenRules));
                return null;
            }
        }



        public bool PromotionInstanceForUserPromotion(int promotionInstanceId, out PromotionInstance pi)
        {
            pi = null;
            // see if we can find the instance
            var x = this.dataService.Invitation.Find(promotionInstanceId);
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
            var y = this.dataService.Invitation.Where(x => x.Recipient.FacebookLogon.FacebookId == this.Account.FacebookLogon.FacebookId);

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
            var y = this.dataService.Invitation.Where(x => x.Recipient.FacebookLogon.FacebookId == this.Account.FacebookLogon.FacebookId
                                                                && x.Promotion.Event.Id == eventId).OrderBy(x => x.CreateDateTime);

            if (y != null)
            {
                return y.ToList<PromotionInstance>();
            }
            return null;
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
            var success = this.dataService.Invitation.SaveOrUpdate(pi, out brokenRules);

            if (success)
            {
                this.dataService.Commit();
                this.log.Info("[] - {0} Succesfully saved invitation {1}", Account, pi);
                // TODO: this should not be here, we should decouple facebook stuff
                ExecutePlugins(t => t.Response(pi));
                return pi;
            }
            else
            {
                this.dataService.Rollback();
                this.log.Debug("[] - {0} Unable to create activity {1}. Violated rules {2}", Account, pi, brokenRules.First());
                return null;
            }
        }
    }
}
