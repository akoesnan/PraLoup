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
    public class ActivityActions : ActionBase<IActivityAction>
    {
        public ActivityActions(Account account, IDataService dataService, IEnumerable<IActivityAction> activityActionPlugins, ILogger log)
            : base(account, dataService, log, activityActionPlugins)
        {
        }

        public Activity CreateActivityFromExistingEvent(Event evt, Privacy p)
        {
            var actv = new Activity(this.account, evt, p);
            IEnumerable<string> brokenRules;
            var success = this.dataService.Activity.SaveOrUpdate(actv, out brokenRules);
            if (success)
            {
                this.dataService.Commit();
                this.log.Info("[] - {0} Succesfully created activity {1}", account, actv);
            }
            else
            {
                this.log.Debug("[] - {0} Unable to create activity {1}. Violated rules {2}", account, actv, brokenRules.First());
            }

            // TODO: this should not be here, we should decouple facebook stuff
            ExecutePlugins(t => t.CreateActivityFromExistingEvent(actv));

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
                this.log.Debug("Unable to save event {0}. Violated rule {1}", evt, String.Join(",", brokenRules));
                return null;
            }
        }

        public IEnumerable<Invitation> Invite(Activity actv, IEnumerable<AccountBase> invites, string message)
        {
            if (actv == null)
            {
                throw new ArgumentException("activity should not be null");
            }

            if (invites == null || invites.Count() == 0)
            {
                throw new ArgumentException("there should be one or more invites");
            }

            var invitations = from i in invites
                              select new Invitation(this.account, i.GetAccount(), actv, message);

            IEnumerable<string> brokenRules;
            var success = this.dataService.Invitation.SaveOrUpdateAll(invitations, out brokenRules);
            if (success)
            {
                this.log.Info("Sucessfully saving {0} invitations for {1}", invitations.Count(), actv);
                ExecutePlugins(f => f.Invite(actv, invitations));
                this.dataService.Commit();
                return invitations;
            }
            else
            {
                this.log.Debug("Error: Unable to save invitations broken rules {0}", String.Join(",", brokenRules));
                return null;
            }
        }

        public Invitation Response(Invitation invitation, InvitationReponseType responseType, string message)
        {
            if (invitation.Recipient.Id != this.account.Id)
            {
                throw new ArgumentException(String.Format("this invitation is not for user {0}", this.account));
            }
            invitation.InvitationResponse.InvitationResponseType = responseType;
            invitation.InvitationResponse.Message = message;
            invitation.CreateDateTime = DateTime.UtcNow;
            return invitation;
        }

        public bool IsInvited(Activity activity)
        {
            var result = this.dataService.Invitation.FirstOrDefault(i => i.Activity == activity && i.Recipient == this.account);

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
            return this.dataService.Activity.Where(a => a.Organizer == this.account).Skip(pageStart).Take(pagecount);
        }

        public IEnumerable<Activity> GetAllActivies()
        {
            return this.dataService.Activity.GetAll();
        }
        public IEnumerable<Activity> GetActivies(Expression<Func<Activity, bool>> predicate)
        {
            return this.dataService.Activity.Where(predicate);
        }
    }
}
