using System.Collections.Generic;
using System.Linq;
using PraLoup.BusinessLogic.Plugins;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;
using PraLoup.DataAccess.Services;
using PraLoup.Infrastructure.Logging;

namespace PraLoup.BusinessLogic
{
    public class BusinessActions : ActionBase<IBusinessAction>
    {
        public BusinessActions(Account account, IDataService dataService, ILogger log, IEnumerable<IBusinessAction> actionPlugins)
            : base(account, dataService, log, actionPlugins)
        {
        }

        public Business GetBusiness(object key)
        {
            return this.dataService.Business.Find(key);
        }

        public IEnumerable<Business> GetBusinesses(int pageCount, int skipCount)
        {
            return this.dataService.Business.GetQuery().Skip(skipCount).Take(pageCount);
        }

        public IEnumerable<Business> GetBusinessForUser(Account user)
        {
            IEnumerable<BusinessUser> f = this.dataService.BusinessUser.Where(b => b.User.Id == user.Id);
            if (f == null || f.Count() == 0)
            {
                return new List<Business>();
            }
            return this.dataService.Business.Where(t => f.Any(c => t.BusinessUsers.Contains(c)));
        }

        public Business CreateBusiness(Business business, Role role)
        {

            business.BusinessUsers = business.BusinessUsers ?? new List<BusinessUser>();
            business.BusinessUsers.Add(new BusinessUser(this.Account, role));
            return SaveOrUpdateBusiness(business);
        }

        public Business SaveOrUpdateBusiness(Business business)
        {
            IEnumerable<string> brokenRules;
            var success = this.dataService.Business.SaveOrUpdate(business, out brokenRules);
            if (success)
            {                
                this.log.Info("Succesfully created business {0}", business);
                return business;
            }
            else
            {
                this.log.Debug("Unable to create business {0}. Validation {1}", business, brokenRules);
                return null;
            }
        }

        public Business AddBusinessUser(Business business, Account account, Role role)
        {
            business.BusinessUsers = business.BusinessUsers ?? new List<BusinessUser>();
            var bu = business.BusinessUsers.FirstOrDefault(u => u.User.Id == account.Id);
            if (bu == null)
            {
                business.BusinessUsers.Add(new BusinessUser(this.Account, role));
            }
            else if (bu.Role != role)
            {
                bu.Role = role;
            }
            return this.SaveOrUpdateBusiness(business);
        }
    }
}
