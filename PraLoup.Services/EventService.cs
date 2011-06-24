using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Interfaces;

namespace PraLoup.Services
{
    public class EventService
    {
        IRepository Repository { get; set; }

        public EventService(IRepository gr)
        {
            this.Repository = gr;
        }

        public Event Save(Event activity)
        {
            if (activity.Id == 0)
            {
                this.Repository.Add(activity);
            }
            else
            {
                var a = this.Repository.Find<Event>(activity.Id);
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
            return this.Repository.Find<Event>(activity.Id);
        }

        public Event Find(Event activity)
        {
            return this.Repository.Find<Event>(activity.Id);
        }
    }
}
