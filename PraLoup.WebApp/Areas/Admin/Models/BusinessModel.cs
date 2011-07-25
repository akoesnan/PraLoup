using System;
using PraLoup.BusinessLogic;
using Entities = PraLoup.DataAccess.Entities;

namespace PraLoup.WebApp.Areas.Admin.Models
{
    public class BusinessModel : BaseAdminModel
    {
        public Entities.Business Business { get; private set; }
        public AccountBase AccountBase { get; private set; }
        public Guid BusinessId { get; private set; }

        public BusinessModel(AccountBase accountBase, Guid id)
        {
            this.AccountBase = accountBase;
            this.BusinessId = id;

            Setup();
        }

        private void Setup()
        {
            var b = this.AccountBase.BusinessActions.GetBusiness(this.BusinessId);
            if (b != null)
            {
                this.Business = b;
                this.IsValid = true;
            }
        }
    }
}