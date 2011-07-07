using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.DataAccess.Entities
{
    public class Address
    {
        public int AddressId { get; set; }

        public string StreetLine1 { get; set; }

        public string StreetLine2 { get; set; }

        public string PhoneNumber { get; set; }
        
        public string PostalCode { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public float Lat { get; set; }

        public float Lon { get; set; }

        public string Neighboorhood { get; set; }
    }
}