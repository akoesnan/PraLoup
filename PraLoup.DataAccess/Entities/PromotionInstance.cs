using System;
using System.Collections.Generic;
using PraLoup.DataAccess.Enums;

namespace PraLoup.DataAccess.Entities
{
    public class PromotionInstance : BaseEntity
    {
        public PromotionInstance()
        {

        }

        public PromotionInstance(Account sender, Account recipient, Promotion ev, Deal deal, string message)
        {
            this.Promotion = ev;
            this.Deal = deal;
            this.Recipient = recipient;
            this.Sender = sender;
            this.Message = message;
            this.CreateDateTime = DateTime.UtcNow;
            // by default response is null
            this.Status = new PromotionInstanceStatus();
        }

        public virtual Promotion Promotion { get; set; }

        public virtual Deal Deal { get; set; }

        public virtual Account Sender { get; set; }

        public virtual Account Recipient { get; set; }

        public virtual PromotionInstanceStatus Status { get; set; }

        public virtual string Message { get; set; }

        public virtual IList<PromotionInstance> ForwardedPromotionInstances { get; set; }

        public virtual IList<Coupon> Coupons { get; set; }

        public virtual bool IsAttended { get; set; }

        public virtual IList<UserRating> UserRatings { get; set; }

        public virtual ConnectionType ConnectionType { get; set; }

        public virtual DateTime CreateDateTime { get; set; }

        public virtual IEnumerable<Comment> Comments { get; set; }



        public virtual Permission Permission { get; set; }
    }
}