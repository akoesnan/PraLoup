using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PraLoup.BusinessLogic.Plugins;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Services;
using PraLoup.Infrastructure.Logging;

namespace PraLoup.BusinessLogic
{
    public class PromotionActions : ActionBase<IEventAction>
    {
        public PromotionActions(Account account, IDataService dataService, ILogger log, IEnumerable<IEventAction> actionPlugins)
            : base(account, dataService, log, actionPlugins)
        {
        }

        public Promotion SavePromotion(Promotion p)
        {
            IEnumerable<string> brokenRules;
            var success = this.dataService.Promotion.SaveOrUpdate(p, out brokenRules);
            if (success)
            {
                this.dataService.Commit();
                this.log.Info("Succesfully created event {0}", p);
                return p;
            }
            else
            {
                this.log.Debug("Unable to create event {0}. Validation {1}", p, brokenRules);
                return null;
            }
        }

        /// <summary>
        /// Be Very careful of calling this method. 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Promotion> GetAllPromotionsForBusiness(Guid businessId)
        {
            return this.dataService.Promotion.Where(p => p.Business.Id == businessId);
        }

        /// <summary>
        /// Be Very careful of calling this method. 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Promotion> GetAllPromotions()
        {
            return this.dataService.Promotion.GetAll();
        }

        public IEnumerable<Promotion> GetPromotions(Expression<Func<Promotion, bool>> predicate)
        {
            return this.dataService.Promotion.Where(predicate);
        }

        public Promotion GetPromotion(Object key)
        {
            var e = this.dataService.Promotion.Find(key);
            return e;

        }

    }
}