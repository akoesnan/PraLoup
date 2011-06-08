using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PraLoup.DataAccess.Entities
{
    public class Invitation : Post
    {
        public Invitation() 
        { 
        
        }

        public Invitation(Account organizer, IEnumerable<Account> recipients, Activity activity, string message)
        {
            this.Activity = activity;
            this.Message = message;
            this.Recipients = recipients;
            this.Sender = organizer;
            this.CreateDateTime = DateTime.UtcNow;
            // by default response is null
            this.Responses = null;
        }

        public Activity Activity { get; set; }

        public Account Sender { get; set; }

        public IEnumerable<Account> Recipients { get; set; }

        public IEnumerable<InvitationResponse> Responses { get; set; }

        public string Message { get; set; }
    }
}