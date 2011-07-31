
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PraLoup.WebApp.Resources;
namespace PraLoup.WebApp.Models.Entities
{
    public class Promotion
    {
        public Promotion()
        {
            this.Event = new Event();
            this.Deals = new List<Deal>();
        }

        public Promotion(Business business)
            : this()
        {
            this.Business = business;
            this.Event = new Event(business);
        }

        public Business Business { get; private set; }

        [Display(ResourceType = typeof(LocStrings), Name = "PromoEvent", Description = "PromoEventDesc")]
        public virtual Event Event { get; set; }

        [Display(ResourceType = typeof(LocStrings), Name = "PromoDeal", Description = "PromoDealDesc")]
        public virtual IList<Deal> Deals { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocStrings), ErrorMessageResourceName = "PromoAvailableReq")]
        [Display(ResourceType = typeof(LocStrings), Name = "PromoAvailable", Description = "PromoAvailableDesc")]
        public virtual int Available { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocStrings), ErrorMessageResourceName = "PromoMaxReferalReq")]
        [Display(ResourceType = typeof(LocStrings), Name = "PromoMaxReferal", Description = "PromoMaxReferalDesc")]
        public virtual byte MaxReferal { get; set; }

    }
}
