using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.DataAccess.Entities
{
    public class MetroArea
    {
        public int Id { get; set; }
        
        // TODO: is this supposed to be a collection of cities Seattle Metro contains many cities 
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public string DisplayedName
        {
            get
            {
                // TODO: figure out what is the correct display for 
                return this.City;
            }
        }
    }
}
