using System;
using System.Collections.Generic;
using PraLoup.DataAccess.Entities;
using PraLoup.Utilities;
using PraLoup.DataPurveyor.Converter;

namespace PraLoup.DataPurveyor.Service
{
    public class GrouponService : IEventService
    {
        private const string apiKey = "96df2a5ac1c730b86506dc349be6627e95945ee7";

        private const string DealsApiUrl = "https://api.groupon.com/v2/deals.json?division_id={0}&client_id={1}";
        private static string DivisionApiUrl = String.Format("https://api.groupon.com/v2/divisions.json?client_id={0}", apiKey);

        // TODO: make use of unity
        public IEventConverter EventConverter { get; set; }

        public GrouponService()
        {
            this.EventConverter = new GrouponConverter();
        }

        public GrouponService(IEventConverter eventConverter)
        {
            this.EventConverter = eventConverter;
        }

        public IEnumerable<Event> GetEventData(string city)
        {
            // TODO: we need to cache this..

            var id = GetGrouponDivision(city);

            if (!String.IsNullOrEmpty(id))
            {
                return this.GetDataFromDivisionId(id);
            }
            else
            {
                return null;
            }

        }

        public IEnumerable<Event> GetDataFromDivisionId(string divisionId)
        {
            var url = String.Format(DealsApiUrl, divisionId, apiKey);
            var response = HttpRequestHelper.GetJsonResponse(url);
            var l = new List<Event>();
            foreach (var d in response.deals)
            {
                l.Add(EventConverter.GetEventObject(d));
            }
            return l;
        }

        public bool IsSelected(Event e)
        {
            // TODO: only return true if we want this lets look at the tags
            return true;
        }

        public string GetGrouponDivision(string city)
        {
            try
            {
                var response = HttpRequestHelper.GetJsonResponse(DivisionApiUrl);
                foreach (var d in response.divisions)
                {
                    if (String.Equals(d.name as string, city, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return d.id;
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return null;
        }
    }
}
