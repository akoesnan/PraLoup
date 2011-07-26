using System;

namespace PraLoup.DataAccess.Entities
{
    public class Deal : BaseEntity
    {
        public virtual decimal OriginalValue { get; set; }

        public virtual decimal CurrentValue { get; set; }

        public virtual decimal Saving { get; set; }

        public virtual DateTime StartDateTime { get; set; }

        public virtual DateTime EndDateTime { get; set; }

        public virtual string Description { get; set; }

        public virtual string RedemptionInstructions { get; set; }

        public virtual string FinePrint { get; set; }

        public virtual int Available { get; set; }

        public virtual int Taken { get; set; }

        public virtual int Score { get; set; }
    }
}
