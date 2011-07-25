using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PraLoup.BusinessLogic.Plugins;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;
using PraLoup.DataAccess.Query;
using PraLoup.DataAccess.Services;
using PraLoup.Infrastructure.Logging;

namespace PraLoup.BusinessLogic
{
    public class EventActions : ActionBase<IEventAction>
    {
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

        public IEnumerable<Event> GetMyEvents()
        {
            return GetMyEvents(0, 10);
        }

        public IEnumerable<Event> GetMyEvents(int pagecount)
        {
            return GetMyEvents(0, pagecount);
        }

        public IEnumerable<Event> GetMyEvents(int pageStart, int pagecount)
        {
            var events = this.dataService.Event.Where(e => e.Organizers.Contains(this.Account)).Skip(pageStart).Take(pagecount).ToList();
            foreach (var e in events)
            {
                e.Permission = Permission.GetPermissions(e, ConnectionType.Owner);
                e.ConnectionType = ConnectionType.Owner;
            }
            return events;
        }

        public IEnumerable<Event> GetMyFriendsEvents(int pageStart, int pagecount)
        {
            var q = new EventOrganizedByFriendQuery(this.Account);
            var events = this.dataService.Event.ExecuteQuery(q).Skip(pageStart).Take(pagecount).List();
            foreach (var e in events)
            {
                e.Permission = Permission.GetPermissions(e, ConnectionType.Friend);
                e.ConnectionType = ConnectionType.Friend;
            }
            return events;
        }

        public IEnumerable<Event> GetMyFriendsOfFriendsEvents(int pageStart, int pagecount)
        {
            var q = new EventOrganizedByFriendOfFriendQuery(this.Account);
            var events = this.dataService.Event.ExecuteQuery(q).Skip(pageStart).Take(pagecount).List();
            foreach (var e in events)
            {
                e.Permission = Permission.GetPermissions(e, ConnectionType.FriendOfFriend);
                e.ConnectionType = ConnectionType.FriendOfFriend;
            }
            return events;
        }

        public IEnumerable<Event> GetPublicEvents(int pageStart, int pagecount)
        {
            var events = this.dataService.Event.Where(e => e.Privacy == Privacy.Public).ToList();
            foreach (var e in events)
            {
                e.Permission = Permission.GetPermissions(e, ConnectionType.NoConnection);
                e.ConnectionType = ConnectionType.NoConnection;
            }
            return events;
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
            var e = this.dataService.Event.Find(eventId);
            if (e != null)
            {

                e.Permission = Permission.GetPermissions(e, ConnectionType.Owner);
            }
            return e;

        }

    }
}