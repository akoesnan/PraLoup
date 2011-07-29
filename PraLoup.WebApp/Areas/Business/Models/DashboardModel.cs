using System;
using PraLoup.BusinessLogic;
using PraLoup.DataAccess.Enums;
using Entities = PraLoup.DataAccess.Entities;

namespace PraLoup.WebApp.Areas.Business.Models
{
    public class DashboardModel
    {
        private AccountBase AccountBase;
        public Role Role { get; private set; }
        public Entities.Business Business { get; private set; }
        private Guid BusinessId;

        public PromotionList PromotionList {get; set;}


    }
}