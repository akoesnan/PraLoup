using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PraLoup.DataAccess.Enums;


namespace PraLoup.DataAccess.Entities
{
    public class InvitationResponse : Post
    {        
        public InvitationResponse() {
            this.InvitationResponseType = InvitationReponseType.NotResponded;
        }

        public virtual InvitationReponseType InvitationResponseType { get; set; }

        public virtual string Message { get; set; }        
    }

}