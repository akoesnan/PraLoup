using System.Dynamic;
using PraLoup.DataAccess.Entities;
using PraLoup.Utilities;

namespace PraLoup.FacebookObjects
{
    public static class ActivityExtensions
    {
        public static object ToFacebookEvent(this Event ev)
        {
            dynamic fbEvent = new ExpandoObject();

            fbEvent.name = ev.Name;
            fbEvent.description = ev.Description;
            fbEvent.start_time = ev.StartDateTime;
            fbEvent.end_time = ev.EndDateTime;
            fbEvent.location = ev.Venue.DisplayedName;

            fbEvent.venue = new ExpandoObject();
            fbEvent.venue.street = ev.Venue.StreetLine1;
            fbEvent.venue.city = ev.Venue.City;
            fbEvent.venue.state = ev.Venue.State;
            fbEvent.venue.country = ev.Venue.Country;
            fbEvent.venue.latitude = ev.Venue.Lat;
            fbEvent.venue.longitude = ev.Venue.Lon;

            fbEvent.privacy = ev.Privacy.GetFacebookValue();
            fbEvent.updated_time = ev.LastUpdatedDateTime;
            return fbEvent;
        }
    }
}
