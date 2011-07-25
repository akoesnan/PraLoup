using System;
using PraLoup.BusinessLogic;
using PraLoup.DataAccess.Enums;
using Entities = PraLoup.DataAccess.Entities;

namespace PraLoup.WebApp.Areas.Business.Models
{
    public class BusinessModel : BaseBusinessModel
    {
        private AccountBase AccountBase;
        public Role Role { get; private set; }
        public Entities.Business Business { get; private set; }
        private Guid BusinessId;

        public BusinessModel(AccountBase accountBase, Guid businessId)
        {
            this.AccountBase = accountBase;
            this.BusinessId = businessId;

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