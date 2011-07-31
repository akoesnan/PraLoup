using System;


namespace PraLoup.WebApp.Models.Entities
{
    public class Venue : Address
    {
        public Venue()
            : base()
        { }

        public Venue(Business b)
            : base()
        {
            this.Name = b.Name;
            this.StreetLine1 = b.Address.StreetLine1;
            this.StreetLine2 = b.Address.StreetLine2;
            this.City = b.Address.City;
            this.UsState = b.Address.UsState;
            this.NonUsState = b.Address.NonUsState;
            this.Country = b.Address.Country;
        }

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
