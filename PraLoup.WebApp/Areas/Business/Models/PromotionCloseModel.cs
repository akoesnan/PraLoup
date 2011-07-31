using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PraLoup.DataAccess.Entities;

namespace PraLoup.WebApp.Areas.Business.Models
{
    public class PromotionCloseModel 
    {
        public Promotion Promotion { get; set; }
        
        public IList<UserCloseData> UserCloseData { get; set; }

    }
}
