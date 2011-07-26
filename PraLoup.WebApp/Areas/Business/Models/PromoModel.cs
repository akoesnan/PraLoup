using System;
using Entities = PraLoup.DataAccess.Entities;
namespace PraLoup.WebApp.Areas.Business.Models
{
    public class PromoModel
    {
        private Guid PromoId;
        private BusinessLogic.AccountBase AccountBase;
        public Entities.Promotion Promotion { get; private set; }

        public PromoModel(BusinessLogic.AccountBase accountBase, Guid promoId)
        {
            // TODO: Complete member initialization
            this.AccountBase = accountBase;
            this.PromoId = promoId;

            Setup();
        }

        public void Setup()
        {
            var promo = this.AccountBase.PromotionActions.GetPromotion(PromoId);
            if (promo != null)
                this.Promotion = promo;
        }
    }
}