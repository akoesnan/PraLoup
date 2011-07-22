using System;
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
            this.InvitationResponse = new InvitationResponse();
        }

        public virtual Activity Activity { get; set; }

        public virtual Account Sender { get; set; }

        public virtual Account Recipient { get; set; }

        public virtual InvitationResponse InvitationResponse { get; set; }

        public virtual string Message { get; set; }

        public virtual Permission Permission { get; set; }

        public virtual ConnectionType ConnectionType { get; set; }

        public virtual void Response(InvitationReponseType responseType, string message)
        {
            this.InvitationResponse.InvitationResponseType = responseType;
            this.InvitationResponse.Message = message;
        }
    }
}