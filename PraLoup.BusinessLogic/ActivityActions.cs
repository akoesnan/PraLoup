using System;
using System.Collections.Generic;
using System.Linq;
using PraLoup.BusinessLogic.Plugins;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;
using PraLoup.DataAccess.Query;
using PraLoup.DataAccess.Services;
using PraLoup.Infrastructure.Logging;

namespace PraLoup.BusinessLogic
{
    public class ActivityActions : ActionBase<IActivityAction>
    {
        public ActivityActions(IDataService dataService, IEnumerable<IActivityAction> activityActionPlugins, ILogger log)
            : base(dataService, log, activityActionPlugins)
        {
        }

        public Activity CreateActivityFromExistingEvent(Event evt, Privacy p)
        {
            var actv = new Activity(this.Account, evt, p);
            IEnumerable<string> brokenRules;
            var success = this.dataService.Activity.SaveOrUpdate(actv, out brokenRules);

            // TODO: this should not be here, we should decouple facebook stuff
            ExecutePlugins(t => t.CreateActivityFromExistingEvent(actv));

            // if there are different 
            success = this.dataService.Activity.SaveOrUpdate(actv, out brokenRules);

            if (success)
            {
                this.dataService.Commit();
                this.log.Info("[] - {0} Succesfully created activity {1}", Account, actv);
            }
            else
            {
                this.dataService.Rollback();
                this.log.Debug("[] - {0} Unable to create activity {1}. Violated rules {2}", Account, actv, brokenRules.First());
            }

            return actv;
        }

        public Activity CreateActivityFromNotExistingEvent(Event evt, Privacy p)
        {
            evt.Privacy = p;
            IEnumerable<string> brokenRules;

            var success = this.dataService.Event.SaveOrUpdate(evt, out brokenRules);
            if (success)
            {
                this.dataService.Commit();
                this.log.Info("Success: event {0} created", evt);
                return CreateActivityFromExistingEvent(evt, p);
            }
            else
            {
                this.dataService.Rollback();
                this.log.Debug("Unable to save event {0}. Violated rule {1}", evt, String.Join(",", brokenRules));
                return null;
            }
        }

        public IEnumerable<Invitation> GetAllInvitationsForActivity(Activity activity)
        {
            return this.dataService.Invitation.Where(i => i.Activity == activity);
        }

        public IEnumerable<Invitation> GetAcceptedInvitationsForActivity(Activity activity, InvitationResponse response)
        {
            return GetAllInvitationsForActivity(activity).Where(i => i.InvitationResponse == response);
        }

        public bool IsInvited(Activity activity)
        {
            var result = this.dataService.Invitation.FirstOrDefault(i => i.Activity == activity && i.Recipient == this.Account);

            return result != null;
        }

        public IEnumerable<Activity> GetMyActivities()
        {
            return GetMyActivities(0, 10);
        }

        public IEnumerable<Activity> GetMyActivities(int pagecount)
        {
            return GetMyActivities(0, pagecount);
        }

        public IEnumerable<Activity> GetMyActivities(int pageStart, int pagecount)
        {
            var activities = this.dataService.Activity.Where(a => a.Organizer == this.Account).Skip(pageStart).Take(pagecount).ToList();
            foreach (var a in activities)
            {
                a.Permission = Permission.GetPermissions(a, ConnectionType.Owner);
            }
            return activities;
        }

        public IEnumerable<Activity> GetAllActivies()
        {
            return this.dataService.Activity.GetAll();
        }

        public IEnumerable<Activity> GetFriendsActivities()
        {
            return GetFriendsActivities(0, 10);
        }

        public IEnumerable<Activity> GetFriendsActivities(int pagecount)
        {
            return GetFriendsActivities(0, pagecount);
        }

        public IEnumerable<Activity> GetFriendsActivities(int pageStart, int pagecount)
        {
            var q = new ActivityOrganizedByFriendQuery(this.Account);
            var activities = this.dataService.Activity.ExecuteQuery(q).Skip(pageStart).Take(pagecount).List();
            foreach (var a in activities)
            {
                a.Permission = Permission.GetPermissions(a, ConnectionType.Friend);
            }
            return activities;
        }

        public IEnumerable<Activity> GetFriendsOfFriendsActivities()
        {
            return GetFriendsOfFriendsActivities(0, 10);
        }

        public IEnumerable<Activity> GetFriendsOfFriendsActivities(int pagecount)
        {
            return GetFriendsOfFriendsActivities(0, pagecount);
        }

        public IEnumerable<Activity> GetFriendsOfFriendsActivities(int pageStart, int pagecount)
        {
            var q = new ActivityOrganizedByFriendOfFriendQuery(this.Account);
            var activities = this.dataService.Activity.ExecuteQuery(q).Skip(pageStart).Take(pagecount).List();
            foreach (var a in activities)
            {
                a.Permission = Permission.GetPermissions(a, ConnectionType.FriendOfFriend);
            }
            return activities;
        }

        public Activity GetActivy(object key)
        {
            var actv = this.dataService.Activity.Find(key);
            if (actv != null)
            {
                actv.ConnectionType = this.Account.GetConnection(actv, this.dataService);
                actv.Permission = Permission.GetPermissions(actv, actv.ConnectionType);
            }
            return actv;
        }

        public Activity SendUpdate(Activity activity)
        {
            throw new NotImplementedException();
        }

        public Activity SendReminder(Activity activity)
        {
            throw new NotImplementedException();
        }
    }
}
