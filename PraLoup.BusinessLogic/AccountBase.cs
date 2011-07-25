using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Web.Security;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Services;
using PraLoup.FacebookObjects;
using PraLoup.Infrastructure.Logging;

namespace PraLoup.BusinessLogic
{
    public class AccountBase : MembershipUser
    {
        public Account Account { get; protected set; }

        internal IDataService DataService { get; set; }

        public FacebookAccount FacebookAccount { get; set; }

        public PromotionActions PromotionActions { get; private set; }
        public PromotionInstanceActions PromotionInstanceActions { get; private set; }
        public BusinessActions BusinessActions { get; private set; }
        public EventActions EventActions { get; private set; }

        public ILogger Log { get; set; }

        public AccountBase(IDataService dataService,
            EventActions eventActions,
            BusinessActions businessActions,
            PromotionActions promoActions,
            PromotionInstanceActions promoInstanceActions,
            ILogger log)
        {
            this.DataService = dataService;
            this.EventActions = eventActions;
            this.BusinessActions = businessActions;
            this.PromotionActions = promoActions;
            this.PromotionInstanceActions = promoInstanceActions;
            this.Log = log;
        }

        public void SetupActionAccount()
        {
            this.SetupFacebookAccount();
            this.SetActionsAccount(this.Account);
        }

        private void SetActionsAccount(Account account)
        {
            this.EventActions.Account = account;
        }

        private Account FindById(int id)
        {
            return this.DataService.Account.Find(id);
        }

        private Account GetAccountByFacebookId(long userName)
        {
            return this.DataService.Account.FirstOrDefault(a => userName == a.FacebookLogon.FacebookId);
        }

        public Account SetupFacebookAccount()
        {
            Contract.Assert(FacebookAccount.HasFacebookAccessToken);
            this.FacebookAccount = new FacebookAccount();
            var acct = this.GetAccountByFacebookId(this.FacebookAccount.Account.FacebookLogon.FacebookId);
            acct = acct ?? this.FacebookAccount.Account;

            IEnumerable<string> brokenRules;
            var success = this.DataService.Account.SaveOrUpdate(this.FacebookAccount.Account, out brokenRules);
            if (success)
            {
                this.DataService.Commit();
                this.Log.Info("Saved facebook login information");
            }
            else
            {
                this.Log.Info("Unable to save facebook logon information");
            }
            this.Account = acct;
            return acct;
        }

        public Account GetAccount()
        {
            return this.Account;
        }
    }
}
