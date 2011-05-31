using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess.Entities;

namespace PraLoup.DataPurveyor.Converter
{
    public class GrouponConverter : IEventConverter
    {
        public Event GetEventObject(dynamic deal)
        {
            var e = new Event();
            if (deal.redemptionLocations != null)
            {
                var location = deal.redemptionLocations[0];
                var v = new Venue();
                v.Lat = location.lat.ToString();
                v.Lon = location.lng.ToString();
                v.City = location.city;
                v.State = location.state;
                v.StreetLine1 = location.streetAddress1;
                v.StreetLine2 = location.streetAddress2;
                v.PhoneNumber = location.phoneNumber;
                v.PostalCode = location.postalCode;
                e.Venue = v;
            }
            e.Name = deal.title;
            e.Url = deal.dealUrl;
            e.ImageUrl = deal.mediumImageUrl;

            var tags = new List<string>();

            foreach (var t in deal.tags)
            {
                tags.Add(t.name);
            }
            e.Tags = tags;
            dynamic options = deal.options;
            if (deal.options != null && options[0] != null)
            {
                e.Price = options[0].price.amount;
                e.Value = options[0].value.amount;
            }
            e.Source = "Groupon";
            return e;
        }
    }
}
