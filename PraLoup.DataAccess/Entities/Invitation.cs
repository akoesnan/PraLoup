using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PraLoup.DataAccess.Enums;

namespace PraLoup.DataAccess.Entities
{
    public class Invitation : Post
    {
        public Invitation() 
        { 
        
        }

        public Invitation(Account sender, Account recipient, Activity activity, string message)
        {
            this.Activity = activity;
            this.Message = message;
            this.Recipient = recipient;
            this.Sender = sender;
            this.CreateDateTime = DateTime.UtcNow;
            // by default response is null
            this.InvitationResponse = null;
        }

        public Activity Activity { get; set; }

        public Account Sender { get; set; }

        public Account Recipient { get; set; }

        public InvitationResponse InvitationResponse { get; set; }

        public string Message { get; set; }

        public void Response (InvitationReponseType responseType, string message) 
        {
            this.InvitationResponse.InvitationResponseType = responseType;
            this.InvitationResponse.Message = message;
        }
    }
}