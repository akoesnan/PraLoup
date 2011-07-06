using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using PraLoup.DataAccess.Entities;
using PraLoup.Utilities;

namespace PraLoup.Facebook
{
    public static class ActivityExtensions
    {
        public static object ToFacebookEvent(this Activity activity)
        {
            dynamic fbEvent = new ExpandoObject();

            fbEvent.name = activity.Event.Name;
            fbEvent.description = activity.Event.Description;
            fbEvent.start_time = activity.Event.StartDateTime;
            fbEvent.end_time = activity.Event.EndDateTime;
            fbEvent.location = activity.Event.Venue.DisplayedName;

            fbEvent.venue = new ExpandoObject();
            fbEvent.venue.street = activity.Event.Venue.StreetLine1;
            fbEvent.venue.city = activity.Event.Venue.City;
            fbEvent.venue.state = activity.Event.Venue.State;
            fbEvent.venue.country = activity.Event.Venue.Country;
            fbEvent.venue.latitude = activity.Event.Venue.Lat;
            fbEvent.venue.longitude = activity.Event.Venue.Lon;

            fbEvent.privacy = activity.Privacy.GetFacebookValue();
            fbEvent.updated_time = activity.UpdatedTime;
            return fbEvent;
        }
    }
}
