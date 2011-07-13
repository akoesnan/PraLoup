using System;
using System.Collections.Generic;
using System.Linq;
using PraLoup.BusinessLogic.Plugins;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;
using PraLoup.DataAccess.Services;
using PraLoup.Infrastructure.Logging;
using System.Linq.Expressions;

namespace PraLoup.BusinessLogic
{
    public class EventActions : ActionBase<IEventAction>
    {
        private Account Account { get; set; }


        public EventActions(Account account, IDataService dataService, ILogger log, IEnumerable<IEventAction> actionPlugins)
            : base(account, dataService, log, actionPlugins)
        {
        }

        public Event SaveEvent(Event ev)
        {
            IEnumerable<string> brokenRules;
            var success = this.dataService.Event.SaveOrUpdate(ev, out brokenRules);
            if (success)
            {
                this.dataService.Commit();
                this.log.Info("Succesfully created event {0}", ev);
                return ev;
            }
            else
            {
                this.log.Debug("Unable to create event {0}. Validation {1}", ev, brokenRules);
                return null;
            }
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return this.dataService.Event.GetAll();
        }

        public IEnumerable<Event> GetEvents(Expression<Func<Event, bool>> predicate)
        {
            return this.dataService.Event.Where(predicate);
        }

        public Event GetEvent(int eventId)
        {
            return this.dataService.Event.Find(eventId);
        }
    }
}