using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.DataAccess.Entities
{
    public class Invitations :  List<Invitation> 
    {
        public Invitations(IEnumerable<Invitation> li) : base(li) { }
        public Invitations() : base() { }
        
    }
    public class InvitationResponses : List<InvitationResponse> 
    {
        public InvitationResponses(IEnumerable<InvitationResponse> li) : base(li) { }
        public InvitationResponses() : base() { }
    }
    public class Accounts : List<Account> 
    {
        public Accounts(IEnumerable<Account> li) : base(li) { }
         public Accounts() : base() { }
    }
}
