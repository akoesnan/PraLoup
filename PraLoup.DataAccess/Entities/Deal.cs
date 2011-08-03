using System;
using System.Collections.Generic;

namespace PraLoup.DataAccess.Entities
{
    public class Deal : BaseEntity
    {
        public virtual string Name { get; set; }

        public virtual decimal OriginalValue { get; set; }

        public virtual decimal DealValue { get; set; }

        public virtual DateTime StartDateTime { get; set; }

        public virtual DateTime EndDateTime { get; set; }

        public virtual string Description { get; set; }

        public virtual string RedemptionInstructions { get; set; }

        public virtual string FinePrint { get; set; }

        public virtual int Available { get; set; }

        public virtual int Taken { get; set; }

        public virtual int Score { get; set; }

        public virtual IList<UserGroup> UserGroup { get; set; }

        public override bool Equals(object obj)
        {
            var d = obj as Deal;
            if (d != null)
            { return this.Id == d.Id; }
            else
            { return false; }
        }

    }
}
