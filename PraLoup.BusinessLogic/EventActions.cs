using System;
using System.Collections.Generic;
using System.Linq;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;
using PraLoup.DataAccess.Interfaces;
using PraLoup.Plugins;

namespace PraLoup.BusinessLogic
{
    public partial class EventActions
    {
        private Account Account { get; set; }
        private IRepository Repository { get; set; }
        public IEnumerable<IEventAction> EventActionPlugins { get; set; }

        public EventActions(Account account, IRepository repository, IEnumerable<IEventAction> eventActionPlugins) {
            this.Account = account;
            this.Repository = repository;
            this.EventActionPlugins = eventActionPlugins;
        }

        public Activity CreateActivityFromExistingEvent(Event evt, Privacy p)
        {
            var actv = new Activity(this.Account, evt, p);
            this.Repository.Add(actv);

            // TODO: this should not be here, we should decouple
            ExecutePlugins(t => t.CreateActivityFromExistingEvent(actv));        

            this.Repository.SaveChanges();
            return actv;
        }

        public Activity CreateActivityFromNotExistingEvent(Event evt, Privacy p)
        {
            evt.Privacy = p;
            this.Repository.Add(evt);
            return CreateActivityFromExistingEvent(evt, p);
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
                              select new Invitation(this.Account, i.GetAccount(), actv, message);
            this.Repository.AddAll(invitations);

            ExecutePlugins(f => f.Invite(actv, invitations));

            this.Repository.SaveChanges();
            return invitations;
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
            return invitation;
        }

        public bool IsInvited(Activity activity)
        {
            var result = this.Repository.FirstOrDefault<Invitation>(i => i.Activity == activity && i.Recipient == this.Account);

            return result != null;
        }

        private void ExecutePlugins<T>(Func<IEventAction, T> f)
        {
            if (EventActionPlugins != null)
            {
                foreach (var plugin in this.EventActionPlugins)
                {
                    f.Invoke(plugin);
                }
            }
        }
    }
}
