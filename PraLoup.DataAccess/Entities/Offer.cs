using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.DataAccess.Entities
{
    public class Offer : BaseEntity
    {
        public virtual decimal OriginalValue { get; set; }

        public virtual decimal Saving { get; set; }

        public virtual decimal CurrentValue { get; set; }

        public virtual DateTime StartDateTime { get; set; }

        public virtual DateTime EndDateTime { get; set; }

        //public virtual IList<string> Highlights { get; set; }

        public virtual string Description { get; set; }

        public virtual string FinePrint { get; set; }

        public virtual int Available { get; set; }

        public virtual int Bought { get; set; }
    }
}
