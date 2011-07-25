using System.Collections.Generic;

namespace PraLoup.DataAccess.Entities
{
    public class Promotion : BaseEntity
    {
        public Promotion() { }

        public Promotion(Business business, Event ev, Deal deal, int available, byte maxReferal)
            : this(business, ev, new Deal[] { deal }, available, maxReferal)
        {
        }

        public Promotion(Business business, Event ev, IList<Deal> deals, int available, byte maxReferal)
        {
            this.Business = business;
            this.Event = ev;
            this.Deals = deals;
            this.Available = available;
            this.MaxReferal = maxReferal;
        }

        public virtual Business Business { get; set; }
        public virtual Event Event { get; set; }
        public virtual IList<Deal> Deals { get; set; }
        public virtual int Available { get; set; }
        public virtual int Taken { get; set; }
        public virtual byte MaxReferal { get; set; }
    }
}
