using System.Collections.Generic;
using PraLoup.DataAccess.Entities;
using PraLoup.Utilities;
using PraLoup.DataPurveyor.Converter;
using System;

namespace PraLoup.DataPurveyor.Service
{
    public class EventfulClient : IEventClient
    {
        public string ApiKey = "cpkPQVxfqTb6KF7d";
        public const string url = "http://api.eventful.com/json/events/search?q=fun&app_key=cpkPQVxfqTb6KF7d";

        private IEventConverter EventConverter { get; set; }

        public EventfulClient()
        {
            this.EventConverter = new EventfulConverter();
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

                //NOTE: event is a reserved word in c# so we need to call GetMember
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
