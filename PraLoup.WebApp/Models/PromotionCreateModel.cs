using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PraLoup.DataAccess.Entities;

namespace PraLoup.WebApp.Models
{
    public class PromotionCreateModel
    {
        public PromotionCreateModel()
        {
            this.Event = new Event();
            this.Event.StartDateTime = DateTime.Now;
            this.Event.EndDateTime = DateTime.Now;
            
            this.Deals = new DealList();
        }
        public PromotionCreateModel(Promotion p)
        {
            this.Event = p.Event;
            this.Deals = new DealList();
            foreach (Deal d in p.Deals)
            {
                this.Deals.Add(d);
            }
            this.Available = p.Available;
            this.MaxReferal = p.MaxReferal;
            this.Taken = p.Taken;
            this.Id = p.Id;
        }

        public Event Event { get; set; }
        public DealList Deals { get; set; }
        public int Available { get; set; }
        public int Taken { get; set; }
        public byte MaxReferal { get; set; }
        public Guid Id { get; set; }

    }
}