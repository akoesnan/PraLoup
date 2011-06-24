using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess.Interfaces;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;

namespace PraLoup.Services
{
    public class InvitationService
    {
        IRepository Repository { get; set; }

        public InvitationService(IRepository gr)
        {
            this.Repository = gr;
        }

        public Invitation Save(Invitation invite)
        {
            if (invite.Id == 0)
            {
                this.Repository.Add(invite);
            }
            else
            {
                var a = this.Repository.Find<Invitation>(invite.Id);
                if (a == null)
                {
                    this.Repository.Add(invite);
                }
                else
                {
                    a = invite;
                }
            }
            this.Repository.SaveChanges();
            // TODO: is thsi necessary
            return this.Repository.Find<Invitation>(invite.Id);
        }

        public Invitation Find(Invitation invite)
        {
            return this.Repository.Find<Invitation>(invite.Id);
        }

        public IEnumerable<Invitation> CreateInvitations(Activity activity, IEnumerable<Account> guesses, string msg)
        {
            var invitations = guesses.Select(g => new Invitation(activity.Organizer, g, activity, msg));
            foreach (var i in invitations)
            {
                this.Save(i);
            }
            return invitations;
        }
    }
}
