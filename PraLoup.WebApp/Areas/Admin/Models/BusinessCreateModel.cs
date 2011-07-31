using System.ComponentModel.DataAnnotations;
using PraLoup.BusinessLogic;
using PraLoup.DataAccess.Enums;
using PraLoup.WebApp.Resources;
using DataEntities = PraLoup.DataAccess.Entities;
using ModelEntities = PraLoup.WebApp.Models.Entities;

namespace PraLoup.WebApp.Areas.Admin.Models
{
    public class BusinessCreateEditModel : BaseAdminModel
    {
        [Display(ResourceType = typeof(LocStrings), Name = "BusinessUserRole", Description = "BusinessUserRoleDesc")]
        public Role Role { get; private set; }
        public ModelEntities.Business Business { get; private set; }
        public AccountBase AccountBase { get; set; }

        public BusinessCreateEditModel()
        {
            this.Business = new ModelEntities.Business();
            this.Role = Role.BusinessAdmin;
        }

        public BusinessCreateEditModel(AccountBase accountBase, ModelEntities.Business business, Role role)
        {
            this.AccountBase = accountBase;
            this.Business = business;
            this.Role = role;
        }

        public bool SaveOrUpdate()
        {
            if (this.Business != null)
            {
                var b = AutoMapper.Mapper.Map<ModelEntities.Business, DataEntities.Business>(this.Business);
                b = this.AccountBase.BusinessActions.CreateBusiness(b, this.Role);
                this.Business = AutoMapper.Mapper.Map<DataEntities.Business, ModelEntities.Business>(b);
                return b != null;
            }
            else
            {
                return false;
            }
        }

    }
}