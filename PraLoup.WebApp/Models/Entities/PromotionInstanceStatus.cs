using System;
using PraLoup.DataAccess.Enums;


namespace PraLoup.WebApp.Models.Entities
{
    public class PromotionInstanceStatus
    {
        public virtual StatusType StatusType { get; set; }

        public virtual string Message { get; set; }

        public virtual Permission Permission { get; set; }

        public virtual DateTime ResponseDateTime { get; set; }
    }
}