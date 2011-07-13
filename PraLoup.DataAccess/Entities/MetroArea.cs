using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.DataAccess.Entities
{
    public class MetroArea : BaseEntity
    {
        public MetroArea()
        {
        }

        public MetroArea(string city, string state, string country)
        {
            this.City = city;
            this.State = state;
            this.Country = country;
        }

        // TODO: is this supposed to be a collection of cities Seattle Metro contains many cities 
        public virtual string City { get; set; }

        public virtual string State { get; set; }

        public virtual string Country { get; set; }

        public virtual string GetDisplayedName()
        {
            // TODO: figure out what is the correct display for 
            return this.City;
        }
    }
}
