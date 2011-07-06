using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;
using PraLoup.DataAccess.Interfaces;

namespace PraLoup.BusinessLogic
{
    public class EventLogic
    {
        IRepository Repository { get; set; }

        public EventLogic(IRepository gr)
        {
            this.Repository = gr;
        }

        public Activity Save(Activity activity)
        {
            if (activity.Id == 0)
            {
                this.Repository.Add(activity);
            }
            else
            {
                var a = this.Repository.Find<Activity>(activity.Id);
                if (a == null)
                {
                    this.Repository.Add(activity);
                }
                else
                {
                    a = activity;

                }
            }
            this.Repository.SaveChanges();
            // TODO: is thsi necessary
            return this.Repository.Find<Activity>(activity.Id);
        }

        public Activity Find(Activity activity)
        {
            return this.Repository.Find<Activity>(activity.Id);
        }
    }
}
