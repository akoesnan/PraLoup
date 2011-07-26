using System;
using PraLoup.BusinessLogic;
using PraLoup.DataAccess.Enums;
using Entities = PraLoup.DataAccess.Entities;

namespace PraLoup.WebApp.Areas.Admin.Models
{
    public class BusinessModel : BaseAdminModel
    {
        public Entities.Business Business { get; private set; }
        public AccountBase AccountBase { get; private set; }
        public Guid BusinessId { get; private set; }
        public Role Role { get; private set; }
        public BusinessModel()
        {
            this.Role = Role.BusinessAdmin;

            Setup();
        }

        public BusinessModel(AccountBase accountBase, Guid id = new Guid(), Role role = Role.BusinessAdmin)
        {
            this.AccountBase = accountBase;
            this.BusinessId = id;

            Setup();
        }

        private void Setup()
        {
            if (this.BusinessId != Guid.Empty)
            {
                var b = this.AccountBase.BusinessActions.GetBusiness(this.BusinessId);
                if (b != null)
                {
                    this.Business = b;
                    this.IsValid = true;
                }
            }
            else
            {
                this.Business = new Entities.Business();
            }
        }
    }
}