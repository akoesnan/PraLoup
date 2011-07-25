using System;
using System.Collections.Generic;
using PraLoup.DataAccess.Enums;

namespace PraLoup.DataAccess.Entities
{
    public class Event : BaseEntity
    {        
        // The event start time
        public virtual DateTime StartDateTime { get; set; }

        public virtual DateTime EndDateTime { get; set; }

        // The event title
        public virtual string Name { get; set; }

        /// <summary>
        /// Events description
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Events privacy
        /// </summary>
        public virtual Privacy Privacy { get; set; }

        /// <summary>
        /// Events Tags
        /// </summary>
        public virtual IList<Tag> Tags { get; set; }

        public virtual Venue Venue { get; set; }

        public virtual Event Parent { get; set; }

        public virtual decimal Price { get; set; }

        public virtual decimal Value { get; set; }

        public virtual IList<Deal> Offers { get; set; }

        public virtual string Url { get; set; }

        public virtual string MobileUrl { get; set; }

        public virtual string ImageUrl { get; set; }

        public virtual string Source { get; set; }

        public virtual float UserRating { get; set; }

        public virtual uint UserReviewsCount { get; set; }

        public virtual IList<Review> UserReviews { get; set; }

        public virtual IList<Account> Organizers { get; set; }

        public virtual DateTime CreatedDateTime { get; set; }

        public virtual Account CreatedBy { get; set; }

        public virtual DateTime LastUpdatedDateTime { get; set; }

        public virtual Account LastUpdatedBy { get; set; }

        public virtual Permission Permission { get; set; }

        public virtual ConnectionType ConnectionType { get; set; }
    }
}