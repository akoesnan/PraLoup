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

        public InvitationReponseType InvitationResponseType { get; set; }

        public string Message { get; set; }        
    }

}