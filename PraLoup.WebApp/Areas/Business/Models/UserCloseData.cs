using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using PraLoup.DataAccess.Entities;


namespace PraLoup.WebApp.Areas.Business.Models
{
    public class UserCloseData
    {
        public Account Account { get; set; }
        public PromotionInstance Invite { get; set; }
        public IList<UserGroup> UserGroups { get; set; }
        public Boolean Redeemed { get; set; }
        public StarModel Rating { get; set; }
        public decimal? AmountSpent { get; set; }
    }
}