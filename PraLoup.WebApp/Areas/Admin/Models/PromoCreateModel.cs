using System;
using PraLoup.BusinessLogic;
using DataEntities = PraLoup.DataAccess.Entities;
using ModelEntities = PraLoup.WebApp.Models.Entities;

namespace PraLoup.WebApp.Areas.Admin.Models
{
    public class PromoCreateModel : BaseAdminModel
    {
        public Guid BusinessId { get; set; }
        public ModelEntities.Promotion Promotion { get; set; }
        public ModelEntities.Business Business { get; set; }
        public AccountBase AccountBase { get; set; }

        public PromoCreateModel()
        {
        }

        public PromoCreateModel(AccountBase accountBase, Guid businessId, ModelEntities.Promotion promotion)
        {
            this.AccountBase = accountBase;
            this.BusinessId = businessId;
            this.Promotion = promotion;
        }

        public void Setup()
        {
            if (this.AccountBase != null && this.BusinessId != null)
            {
                var b = this.AccountBase.BusinessActions.GetBusiness(BusinessId);
                this.Business = AutoMapper.Mapper.Map<DataEntities.Business, ModelEntities.Business>(b);
                this.Promotion = new ModelEntities.Promotion(this.Business);
                this.IsValid = true;
            }
        }

        public bool CreatePromotion()
        {
            if (this.AccountBase != null && this.BusinessId != null)
            {
                var b = this.AccountBase.BusinessActions.GetBusiness(BusinessId);
                if (b != null)
                {
                    var p = AutoMapper.Mapper.Map<ModelEntities.Promotion, DataEntities.Promotion>(this.Promotion);
                    p.Business = b;
                    p = this.AccountBase.PromotionActions.SavePromotion(p);
                    return p != null;
                }
            }
            return false;
        }

    }
}
