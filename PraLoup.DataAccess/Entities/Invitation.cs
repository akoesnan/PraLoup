using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PraLoup.DataAccess.Entities
{
    public class Invitation : Post
    {
        public int Id { get; set; }

        public Activity Activity { get; set; }

        public Account Sender { get; set; }

        public IEnumerable<Account> Recipients { get; set; }

        public IEnumerable<InvitationResponse> Responses { get; set; }

        public string Message { get; set; }
    }
}