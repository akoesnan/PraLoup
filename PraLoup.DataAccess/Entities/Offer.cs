using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.DataAccess.Entities
{
    public class Offer
    {
        public int OfferId { get; set; }

        public decimal OriginalValue { get; set; }
        
        public decimal Saving { get; set; }

        public decimal CurrentValue { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public IEnumerable<string> Highlights { get; set; }

        public string Description { get; set; }

        public string FinePrint { get; set; }

        public int Available { get; set; }

        public int Bought { get; set; }
    }
}
