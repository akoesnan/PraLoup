using System.Collections.Generic;
using PraLoup.DataAccess.Entities;
using PraLoup.Utilities;
using PraLoup.DataPurveyor.Converter;
using System;

namespace PraLoup.DataPurveyor.Service
{
    public class EventfulService : IEventService
    {
        public string ApiKey = "cpkPQVxfqTb6KF7d";
        public const string url = "http://api.eventful.com/json/events/search?q=fun&app_key=cpkPQVxfqTb6KF7d";

        // TODO: make use of unity
        public IEventConverter EventConverter { get; set; }

        public EventfulService()
        {
            this.EventConverter = new EventfulConverter();
        }

        public EventfulService(IEventConverter eventConverter)
        {
            this.EventConverter = eventConverter;
        }

        public IEnumerable<Event> GetEventData(string city)
        {

            // the argument is documented at: http://api.eventful.com/tools/tutorials/search
            var apiUrl = url.AppendQueryString("app_key", ApiKey)
                            .AppendQueryString("l", city)
                            .AppendQueryString("category", city);

            // add the keyword?                
            //.AppendQueryString(
            try
            {
                var response = HttpRequestHelper.GetJsonResponse(apiUrl);

                var events = response.events.GetMember("event");

                var l = new List<Event>();

                foreach (var d in events)
                {
                    l.Add(EventConverter.GetEventObject(d));
                }
                return l;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        
        public bool IsSelected(Event e)
        {
            // TODO: only return true if we want this lets look at the tags
            return true;
        }

    }
}
