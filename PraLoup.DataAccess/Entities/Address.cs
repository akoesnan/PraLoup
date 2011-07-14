using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.DataAccess.Entities
{
    public class Address : BaseEntity
    {
        public virtual string StreetLine1 { get; set; }

        public virtual string StreetLine2 { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual string PostalCode { get; set; }

        public virtual string City { get; set; }

        public virtual string State { get; set; }

        public virtual string Country { get; set; }

        public virtual float Lat { get; set; }

        public virtual float Lon { get; set; }

        public virtual string Neighboorhood { get; set; }
    }
}