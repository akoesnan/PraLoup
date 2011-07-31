using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PraLoup.WebApp.Resources;

namespace PraLoup.WebApp.Models.Entities
{
    public class Deal
    {
        public Deal()
        {
            this.StartDateTime = DateTime.Now;
            this.EndDateTime = DateTime.Now.AddDays(1);
            this.UserGroups = new List<UserGroup>() { new UserGroup() };
        }

        [Required(ErrorMessageResourceType = typeof(LocStrings), ErrorMessageResourceName = "DealNameReq")]
        [Display(ResourceType = typeof(LocStrings), Name = "DealName", Description = "DealNameDesc")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocStrings), ErrorMessageResourceName = "DealOriginalValueReq")]
        [Display(ResourceType = typeof(LocStrings), Name = "DealOriginalValue", Description = "DealOriginalValueDesc")]
        public decimal OriginalValue { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocStrings), ErrorMessageResourceName = "DealValueReq")]
        [Display(ResourceType = typeof(LocStrings), Name = "DealValue", Description = "DealValue")]
        public decimal DealValue { get; set; }

        [Display(ResourceType = typeof(LocStrings), Name = "DealSaving", Description = "DealSavingDesc")]
        [Editable(false)]
        public decimal Saving { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocStrings), ErrorMessageResourceName = "DealStartDateTimeReq")]
        [Display(ResourceType = typeof(LocStrings), Name = "DealStartDateTime", Description = "DealStartDateTimeDesc")]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocStrings), ErrorMessageResourceName = "DealEndDateTimeReq")]
        [Display(ResourceType = typeof(LocStrings), Name = "DealEndDateTime", Description = "DealEndDateTimeDesc")]
        public DateTime EndDateTime { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocStrings), ErrorMessageResourceName = "DealDescriptionReq")]
        [Display(ResourceType = typeof(LocStrings), Name = "DealDescription", Description = "DealDescriptionDesc")]
        public string Description { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocStrings), ErrorMessageResourceName = "DealRedemptionInstructionsReq")]
        [Display(ResourceType = typeof(LocStrings), Name = "DealRedemptionInstructionsValue", Description = "DealRedemptionInstructionsDesc")]
        public string RedemptionInstructions { get; set; }

        [Display(ResourceType = typeof(LocStrings), Name = "DealFinePrint", Description = "DealFinePrintDesc")]
        public string FinePrint { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocStrings), ErrorMessageResourceName = "DealAvailableReq")]
        [Display(ResourceType = typeof(LocStrings), Name = "DealAvailable", Description = "DealAvailableDesc")]
        public int Available { get; set; }

        public IList<UserGroup> UserGroups { get; set; }
    }
}
