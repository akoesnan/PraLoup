using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PraLoup.DataAccess.Entities
{
    public class Invitation : Post
    {

        public Invitation(Account organizer, Account recipient, Activity activity, string message)
        {
            Activity = activity;
            Message = message;
            Recipeint = recipient;
            Sender = organizer;
            CreateDateTime = DateTime.UtcNow;
            // by default response is null
            Response = null;
        }

        public Activity Activity { get; set; }

        public Account Sender { get; set; }

        public Account Recipeint { get; set; }

        public InvitationResponse Response { get; set; }

        public string Message { get; set; }
    }
}