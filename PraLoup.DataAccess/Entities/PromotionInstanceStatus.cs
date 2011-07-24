using System;
using PraLoup.DataAccess.Enums;


namespace PraLoup.DataAccess.Entities
{
    public class PromotionInstanceStatus : BaseEntity
    {
        public PromotionInstanceStatus()
        {
            this.StatusType = StatusType.NotResponded;
        }

        public virtual StatusType StatusType { get; set; }

        public virtual string Message { get; set; }

        public virtual Permission Permission { get; set; }

        public virtual DateTime ResponseDateTime { get; set; }
    }
}