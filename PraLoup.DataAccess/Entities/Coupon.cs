using System;
namespace PraLoup.DataAccess.Entities
{
    public class Coupon : BaseEntity
    {
        public virtual string CouponCode { get; set; }

        public virtual bool Redeemed { get; set; }

        public virtual decimal FullAmount { get; set; }

        public virtual decimal DiscountAmount { get; set; }

        public virtual decimal PaidAmount { get; set; }

        public virtual DateTime RedeemDateTime { get; set; }
    }
}
