using PraLoup.BusinessLogic;
using PraLoup.DataAccess.Enums;
using Entities = PraLoup.DataAccess.Entities;

namespace PraLoup.WebApp.Areas.Admin.Models
{
    public class BusinessCreateModel : BaseAdminModel
    {
        public Role Role { get; private set; }
        public Entities.Business Business { get; private set; }
        public AccountBase AccountBase { get; private set; }

        public BusinessCreateModel(AccountBase accountBase, Entities.Business business, Role role)
        {
            this.AccountBase = accountBase;
            this.Business = business;
            this.Role = role;

            Setup();
        }

        private void Setup()
        {
            throw new System.NotImplementedException();
        }

        private void SaveOrUpdate()
        {
            this.AccountBase.BusinessActions.SaveOrUpdateBusiness(Business);
        }

    }
}