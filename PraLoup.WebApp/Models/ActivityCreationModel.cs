using PraLoup.DataAccess.Entities;
using System.Collections.Generic;

namespace PraLoup.WebApp.Models
{
    public class ActivityCreationModel
    {
        public Activity Activity { get; private set; }
        public Event Event { get; private set; }
        public AccountListJson Invited;
        
        public ActivityCreationModel(Activity activity, Event ev)
        {
            this.Activity = activity;
            this.Event = ev;

            List<Account> la = new List<Account>();

            this.Invited = new AccountListJson(la);
    
        }

        public void InvitePeople()
        {
        }
    }
}