using System;
using System.Collections.Generic;
using PraLoup.BusinessLogic;
using Entities = PraLoup.DataAccess.Entities;
namespace PraLoup.WebApp.Areas.Admin.Models
{
    public class PromoIndexModel : BaseAdminModel
    {
        private Guid BusinessId;
        private AccountBase AccountBase;
        private string BusinessName;
        public IEnumerable<Entities.Promotion> Promotions { get; private set; }


        public PromoIndexModel(AccountBase accountBase, Guid businessId)
        {
            this.AccountBase = accountBase;
            this.BusinessId = businessId;

            Setup();
        }

        public PromoIndexModel(BusinessLogic.AccountBase accountBase, Guid businessId, string businessName)
        {
            // TODO: Complete member initialization
            this.AccountBase = accountBase;
            this.BusinessId = businessId;
            this.BusinessName = businessName;

            Setup();
        }

        public void Setup()
        {
            this.Promotions = this.AccountBase.PromotionActions.GetAllPromotionsForBusiness(BusinessId);
        }
    }
}