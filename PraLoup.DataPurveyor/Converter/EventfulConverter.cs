using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess.Entities;

namespace PraLoup.DataPurveyor.Converter
{
    public class EventfulConverter : IEventConverter
    {
        public Event GetEventObject(dynamic ev)
        {
            var e = new Event();
            e.Name = ev.title;
            e.Description = ev.description;
            e.Url = ev.url;
            e.StartDateTime = DateTime.Parse(ev.start_time);
            e.EndDateTime = !String.IsNullOrEmpty(ev.stop_time) ? DateTime.Parse(ev.stop_time) : default(DateTime);
            if (ev.image != null){
                if (ev.image.medium != null)
                {
                    e.ImageUrl = ev.image.medium.url;
                }
                else if (ev.image.small != null) {
                    e.ImageUrl = ev.image.small.url;
                }
            }
            e.Venue = new Venue()
            {
                StreetLine1 = ev.venue_address,
                City = ev.city_name,
                State = ev.region_name,
                Country = ev.country_name,
                Lon = ev.longitude,
                Lat = ev.latitude,
                Name = ev.venue_name
            };
            e.Source = "Eventful";
            return e;
        }
    }
}
