using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess.Interfaces;
using PraLoup.DataAccess.Entities;

namespace PraLoup.BusinessLogic
{
    public class ActivityLogic
    {
        IRepository Repository;
        public ActivityLogic(IRepository gr) {
            this.Repository = gr;
        }

        public IEnumerable<Invitation> FindInvitation(Activity activity) {
            return this.Repository.Where<Invitation>(i => i.Activity.Id == activity.Id); 
        }
    }
}
