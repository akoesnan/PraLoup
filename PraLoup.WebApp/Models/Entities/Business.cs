using System;
using System.ComponentModel.DataAnnotations;
using PraLoup.DataAccess.Enums;
using PraLoup.WebApp.Resources;

namespace PraLoup.WebApp.Models.Entities
{
    public class Business
    {
        public Business()
        {
            Address = new Address();
        }

        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocStrings), ErrorMessageResourceName = "BusinessNameReq")]
        [Display(ResourceType = typeof(LocStrings), Name = "BusinessName", Description = "BusinessNameDesc")]
        public string Name { get; set; }

        public Address Address { get; set; }

        [Display(ResourceType = typeof(LocStrings), Name = "BusinessPhone", Description = "BusinessPhoneDesc")]
        public string Phone { get; set; }

        [Display(ResourceType = typeof(LocStrings), Name = "BusinessUrl", Description = "BusinessUrlDesc")]
        public string Url { get; set; }

        [Display(ResourceType = typeof(LocStrings), Name = "BusinessEmail", Description = "BusinessEmailDesc")]
        public virtual string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocStrings), ErrorMessageResourceName = "BusinessNameReq")]
        [Display(ResourceType = typeof(LocStrings), Name = "BusinessDesc", Description = "BusinessDescDesc")]
        public virtual string Description { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocStrings), ErrorMessageResourceName = "BusinessCategoryReq")]
        [Display(ResourceType = typeof(LocStrings), Name = "BusinessCategory", Description = "BusinessCategoryDesc")]
        public Category Category { get; set; }

        [Display(ResourceType = typeof(LocStrings), Name = "BusinessImageUrl", Description = "BusinessImageUrlDesc")]
        public string ImageUrl { get; set; }

        [Display(ResourceType = typeof(LocStrings), Name = "BusinessTwitterId", Description = "BusinessTwitterIdDesc")]
        public string TwitterId { get; set; }
    }
}