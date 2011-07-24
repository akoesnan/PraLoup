using System.Dynamic;
using PraLoup.DataAccess.Entities;
using PraLoup.Utilities;

namespace PraLoup.FacebookObjects
{
    public static class ActivityExtensions
    {
        public static object ToFacebookEvent(this Event activity)
        {
            dynamic fbEvent = new ExpandoObject();

            fbEvent.name = activity.Name;
            fbEvent.description = activity.Description;
            fbEvent.start_time = activity.StartDateTime;
            fbEvent.end_time = activity.EndDateTime;
            fbEvent.location = activity.Venue.DisplayedName;

            fbEvent.venue = new ExpandoObject();
            fbEvent.venue.street = activity.Venue.StreetLine1;
            fbEvent.venue.city = activity.Venue.City;
            fbEvent.venue.state = activity.Venue.State;
            fbEvent.venue.country = activity.Venue.Country;
            fbEvent.venue.latitude = activity.Venue.Lat;
            fbEvent.venue.longitude = activity.Venue.Lon;

            fbEvent.privacy = activity.Privacy.GetFacebookValue();
            fbEvent.updated_time = activity.LastUpdatedDateTime;
            return fbEvent;
        }
    }
}
