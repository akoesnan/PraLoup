using System;
using System.Collections.Generic;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Services;
using PraLoup.Infrastructure.Logging;

namespace PraLoup.BusinessLogic
{
    public abstract class ActionBase<T>
    {
        protected ILogger log;
        protected IDataService dataService;
        protected IEnumerable<T> actionPlugins;

        public ActionBase(Account account, IDataService dataService, ILogger log, IEnumerable<T> actionPlugins)
        {
            this.Account = account;
            this.dataService = dataService;
            this.log = log;
            this.actionPlugins = actionPlugins;
        }

        internal Account Account { get; set; }

        protected void ExecutePlugins<TOut>(Func<T, TOut> f)
        {
            if (this.actionPlugins != null)
            {
                foreach (var plugin in this.actionPlugins)
                {
                    f.Invoke(plugin);
                }
            }
        }
    }
}
