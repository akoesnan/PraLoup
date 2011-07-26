using System.Collections.Generic;
using System.Linq;
using PraLoup.BusinessLogic;
using Entities = PraLoup.DataAccess.Entities;

namespace PraLoup.WebApp.Areas.Admin.Models
{
    public class BusinessIndexModel : BaseAdminModel
    {
        private int pageCount;
        private int currentPage;
        private AccountBase AccountBase;
        public IEnumerable<Entities.Business> Businesses { get; private set; }

        public BusinessIndexModel(AccountBase accountBase, int pageCount, int currentPage)
        {
            this.AccountBase = accountBase;
            this.pageCount = pageCount;
            this.currentPage = currentPage;
            Setup();
        }

        public void Setup()
        {
            int skipCount = pageCount * currentPage;
            Businesses = this.AccountBase.BusinessActions.GetBusinesses(pageCount, skipCount).ToList();
        }
    }
}