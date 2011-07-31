using System;
using System.Collections.Generic;
using PraLoup.BusinessLogic;
using DataEntities = PraLoup.DataAccess.Entities;
using ModelEntities = PraLoup.WebApp.Models.Entities;
namespace PraLoup.WebApp.Areas.Admin.Models
{
    public class PromoIndexModel : BaseAdminModel
    {
        private Guid? BusinessId;
        private AccountBase AccountBase;
        private string BusinessName;
        public IEnumerable<ModelEntities.Promotion> Promotions { get; private set; }

        public PromoIndexModel(AccountBase accountBase, Guid businessId)
        {
            this.AccountBase = accountBase;
            this.BusinessId = businessId;

            Setup();
        }

        public PromoIndexModel(BusinessLogic.AccountBase accountBase, Guid? businessId, string businessName)
        {
            // TODO: Complete member initialization
            this.AccountBase = accountBase;
            this.BusinessId = businessId;
            this.BusinessName = businessName;

            Setup();
        }

        public void Setup()
        {
            // lookup using business guid, if its not empty
            if (this.AccountBase == null)
                throw new Exception("AccountBase should be populated");

            if (BusinessId != null && BusinessId != default(Guid))
            {
                var promos = this.AccountBase.PromotionActions.GetAllPromotionsForBusiness(BusinessId.Value);
                this.Promotions = AutoMapper.Mapper.Map<IEnumerable<DataEntities.Promotion>, IEnumerable<ModelEntities.Promotion>>(promos);
            }
            // lookup using business name if it's not null
            else if (!string.IsNullOrEmpty(this.BusinessName))
            {

            }
            else
            {
            }
        }
    }
}