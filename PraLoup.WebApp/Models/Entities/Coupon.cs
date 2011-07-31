using System;
namespace PraLoup.WebApp.Models.Entities
{
    public class Coupon
    {
        public virtual string CouponCode { get; set; }

        public virtual bool Redeemed { get; set; }

        public virtual decimal FullAmount { get; set; }

        public virtual decimal DiscountAmount { get; set; }

        public virtual decimal PaidAmount { get; set; }

        public virtual DateTime RedeemDateTime { get; set; }
    }
}
