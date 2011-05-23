﻿using System;
using System.Collections.Generic;
using PraLoup.DataAccess.Enums;

namespace PraLoup.DataAccess.Entities
{
    public class Event
    {
        public int EventId { get; set; }

        // The event start time
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        // The event title
        public string Name { get; set; }

        /// <summary>
        /// Events description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Events privacy
        /// </summary>
        public Privacy Privacy { get; set; }

        /// <summary>
        /// Events Tags
        /// </summary>
        public IEnumerable<string> Tags { get; set; }        

        public Venue Venue { get; set; }

        public Event Parent { get; set; }

        public decimal Price { get; set; }

        public decimal Value { get; set; }

        public IEnumerable<Offer> Offers { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public string Source { get; set; }
    }
}