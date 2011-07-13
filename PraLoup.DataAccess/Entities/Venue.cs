using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Enums;


namespace PraLoup.DataAccess.Entities
{
    public class Venue : Address
    {
        public virtual string Name { get; set; }

        public virtual string DisplayedName
        {
            get
            {
                if (!String.IsNullOrEmpty(this.Name))
                    return this.Name;
                else if (!String.IsNullOrEmpty(this.City))
                    return this.City;
                else
                    return String.Empty;
            }
        }
    }
}
