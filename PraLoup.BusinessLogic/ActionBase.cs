using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.Infrastructure.Logging;
using PraLoup.BusinessLogic.Plugins;
using PraLoup.DataAccess.Services;
using PraLoup.DataAccess.Entities;

namespace PraLoup.BusinessLogic
{
    public abstract class ActionBase<T>
    {
        protected Account account;
        protected ILogger log;
        protected IDataService dataService;
        protected IEnumerable<T> actionPlugins;

        public ActionBase(Account account, IDataService dataService, ILogger log, IEnumerable<T> actionPlugins)
        {
            this.account = account;
            this.dataService = dataService;
            this.log = log;
            this.actionPlugins = actionPlugins;
        }

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
