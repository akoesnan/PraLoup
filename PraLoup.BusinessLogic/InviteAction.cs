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
    public class InviteAction : ActionBase<IInviteAction>
    {
        public InviteAction(IDataService dataService, IEnumerable<IInviteAction> inviteActionPlugins, ILogger log)
            : base(dataService, log, inviteActionPlugins)
        {
        }

        public IEnumerable<Invitation> Invite(Activity actv, IEnumerable<Account> invites, string message)
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
                              select new Invitation(this.Account, i, actv, message);

            IEnumerable<string> brokenRules;
            var success = this.dataService.Invitation.SaveOrUpdateAll(invitations, out brokenRules);
            if (success)
            {
                this.log.Info("Sucessfully saving {0} invitations for {1}", invitations.Count(), actv);
                ExecutePlugins(f => f.Invite(actv, invites , message));
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
            if (invitation.Recipient.Id != this.Account.Id)
            {
                throw new ArgumentException(String.Format("this invitation is not for user {0}", this.Account));
            }

            invitation.InvitationResponse.InvitationResponseType = responseType;
            invitation.InvitationResponse.Message = message;
            invitation.CreateDateTime = DateTime.UtcNow;

            IEnumerable<string> brokenRules;
            var success = this.dataService.Invitation.SaveOrUpdate(invitation, out brokenRules);

            if (success)
            {
                this.dataService.Commit();
                this.log.Info("[] - {0} Succesfully saved invitation {1}", Account, invitation);
                // TODO: this should not be here, we should decouple facebook stuff
                ExecutePlugins(t => t.Response(invitation));
                return invitation;
            }
            else
            {
                this.dataService.Rollback();
                this.log.Debug("[] - {0} Unable to create activity {1}. Violated rules {2}", Account, invitation, brokenRules.First());
                return null;
            }
        }
    }
}
