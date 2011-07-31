using System;
using System.ComponentModel.DataAnnotations;
using PraLoup.DataAccess.Enums;
using PraLoup.WebApp.Resources;

namespace PraLoup.WebApp.Models.Entities
{
    public class Event
    {
        public Event()
        {
            this.Venue = new Venue();
        }

        public Event(Business business)
        {
            this.Venue = new Venue(business);
            this.StartDateTime = DateTime.Now;
            this.EndDateTime = DateTime.Now.AddDays(1);
        }

        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocStrings), ErrorMessageResourceName = "EventStartTimeReq")]
        [Display(ResourceType = typeof(LocStrings), Name = "EventStartTime")]
        public DateTime? StartDateTime { get; set; }

        [Display(ResourceType = typeof(LocStrings), Name = "EventEndTime", Description = "EventEndTimeDesc")]
        public DateTime? EndDateTime { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocStrings), ErrorMessageResourceName = "EventNameReq")]
        [Display(ResourceType = typeof(LocStrings), Name = "EventName", Description = "EventNameDesc")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(LocStrings), Name = "EventDesc", Description = "EventDescDesc")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(LocStrings), Name = "EventPrivacy", Description = "EventPrivacyDesc")]
        public Privacy Privacy { get; set; }

        public Venue Venue { get; set; }

        [Display(ResourceType = typeof(LocStrings), Name = "EventUrl", Description = "EventUrlDesc")]
        public string Url { get; set; }

        [Display(ResourceType = typeof(LocStrings), Name = "EventImageUrl", Description = "EventImageUrlDesc")]
        public string ImageUrl { get; set; }

    }
}