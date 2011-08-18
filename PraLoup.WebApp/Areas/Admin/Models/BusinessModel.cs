using System;
using System.Collections.Generic;
using System.Linq;
using PraLoup.BusinessLogic;
using PraLoup.DataAccess.Enums;
using DataEntities = PraLoup.DataAccess.Entities;
using ModelEntities = PraLoup.WebApp.Models.Entities;

namespace PraLoup.WebApp.Areas.Admin.Models
{
    public class BusinessModel : BaseAdminModel
    {
        public ModelEntities.Business Business { get; private set; }
        public String Message { get; set; }
        public IEnumerable<ModelEntities.Promotion> UpcomingPromos { get; set; }
        public IEnumerable<ModelEntities.Promotion> CurrentPromos { get; set; }
        public IEnumerable<ModelEntities.Promotion> PastPromos { get; set; }
        public AccountBase AccountBase { get; private set; }
        public Guid BusinessId { get; private set; }
        public Role Role { get; private set; }

        public BusinessModel()
        {
            this.Role = Role.BusinessAdmin;
            Setup();
        }

        public BusinessModel(AccountBase accountBase, Guid id = new Guid(), Role role = Role.BusinessAdmin, String message = null)
        {
            this.AccountBase = accountBase;
            this.BusinessId = id;
            this.Message = message;

            Setup();
        }

        private void Setup()
        {
            if (this.BusinessId != Guid.Empty)
            {
                var b = this.AccountBase.BusinessActions.GetBusiness(this.BusinessId);
                if (b != null)
                {
                    this.Business = AutoMapper.Mapper.Map<DataEntities.Business, ModelEntities.Business>(b);
                    var now = DateTime.UtcNow;
                    var cp = this.AccountBase.PromotionActions.GetCurrentPromos(this.BusinessId, now);
                    var pp = this.AccountBase.PromotionActions.GetPastPromos(this.BusinessId, now);
                    var up = this.AccountBase.PromotionActions.GetUpcomingPromos(this.BusinessId, now);

                    if (cp.Count() > 0)
                    {
                        this.CurrentPromos = AutoMapper.Mapper.Map<IEnumerable<DataEntities.Promotion>, IEnumerable<ModelEntities.Promotion>>(cp); ;
                    }
                    if (up.Count() > 0)
                    {
                        this.UpcomingPromos = AutoMapper.Mapper.Map<IEnumerable<DataEntities.Promotion>, IEnumerable<ModelEntities.Promotion>>(up); ;
                    }
                    if (pp.Count() > 0)
                    {
                        this.PastPromos = AutoMapper.Mapper.Map<IEnumerable<DataEntities.Promotion>, IEnumerable<ModelEntities.Promotion>>(pp); ;
                    }

                    this.IsValid = true;
                }
            }
            else
            {
                this.Business = new ModelEntities.Business();
            }
        }
    }
}