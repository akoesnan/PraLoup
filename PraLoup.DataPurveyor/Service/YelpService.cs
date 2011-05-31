using System;
using System.Collections.Generic;
using PraLoup.DataAccess.Entities;
using PraLoup.DataPurveyor.Converter;
using PraLoup.Utilities;

namespace PraLoup.DataPurveyor.Service
{
    public class YelpService : IEventService
    {
        private IEventConverter EventConverter { get; set; }

        public YelpService()
        {
            this.EventConverter = new YelpConverter();
        }

        const string YelpV1Key = "kj6rXZe1bVgX5Oo7OBOfIg";

        private OAuthData YelpV2OAuth = new OAuthData()
        {
            CustomerKey = "yznXf3SxPdk-dsIELfgIfg",
            CustomerSecret = "RXe9EDqb4zhZXobZewO223mQTuI",
            AccessToken = "EtZODhqeYRer6XrhT9PhX-YQFb56Fvu0",
            AccessTokenSecret = "D79cp5u-qrzZlAAIoXAODv3_xNk",
            SignatureMethod = "HMAC-SHA1"
        };

        const string YelpApiUrl = "http://api.yelp.com/v2/search";

        public IEnumerable<Event> GetEventData(string city)
        {
            var url = YelpApiUrl;
            var urlParams = new Dictionary<string, string>() {
                {"terms", "bars"},
                {"location", city},
                {"sort", "2"}, // 0 best match, 1 distance, 2 highest rating
                {"limit", "20"},
                {"category_filter", "nightlife"} //http://www.yelp.com/developers/documentation/category_list

            };

            url = url.AppandQueryParams(urlParams);
            var oauthUtil = new OAuthBase();

            Console.WriteLine("URL : " + url);
            url = oauthUtil.GetUrl(url, YelpV2OAuth);
            Console.WriteLine("OAuth URL : " + url);


            var converter = new YelpConverter();

            try
            {
                var response = HttpRequestHelper.GetJsonResponse(url);
                var events = new List<Event>();
                foreach (var b in response.businesses)
                {
                    var e = converter.GetEventObject(b);
                    events.Add(e);
                }
                return events;
            }
            catch (Exception e)
            {
                // TODO: Log 
                return null;
            }
        }

        public bool IsSelected(Event e)
        {
            throw new NotImplementedException();
        }
    }
}
