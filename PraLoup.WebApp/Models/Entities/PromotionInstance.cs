using System;
using System.Collections.Generic;
using PraLoup.DataAccess.Enums;

namespace PraLoup.WebApp.Models.Entities
{
    public class PromotionInstance 
    {
        public virtual Promotion Promotion { get; set; }

        public virtual Deal Deal { get; set; }

        public virtual Account Sender { get; set; }

        public virtual Account Recipient { get; set; }

        public virtual PromotionInstanceStatus Status { get; set; }

        public virtual string Message { get; set; }

        public virtual IList<PromotionInstance> ForwardedPromotionInstances { get; set; }

        public virtual IList<Coupon> Coupons { get; set; }

        public virtual bool IsAttended { get; set; }        

        public virtual ConnectionType ConnectionType { get; set; }

        public virtual DateTime CreateDateTime { get; set; }        

        public virtual Permission Permission { get; set; }
    }
}