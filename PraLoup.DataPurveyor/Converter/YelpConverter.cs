using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess.Entities;

namespace PraLoup.DataPurveyor.Converter
{
    public class YelpConverter : IEventConverter
    {

        public Event GetEventObject(dynamic ev)
        {
            var e = new Event();
            e.Description = ev.snippet_text;
            e.Name = ev.name;
            e.Url = ev.url;
            e.MobileUrl = ev.mobile_url;
            e.ImageUrl = ev.image_url;
            e.UserReviewsCount = (uint) ev.review_count;
            e.UserRating = GetUserRating(ev.rating_img_url);

            if (ev.location != null)
            {
                var l = ev.location;
                var v = new Venue();
                v.City = l.city;
                v.StreetLine1 = l.address[0];
                if (l.coordinate != null)
                {
                    v.Lat = l.coordinate.latitude.ToString();
                    v.Lon = l.coordinate.longitude.ToString();

                }
                v.State = l.state_code;
                if (l.neighborhoods != null) 
                v.Neighboorhood = l.neighborhoods[0];                
            }
            return e;
        }

        private static string[] StarUrls = new string[] {
            "stars_5", "stars_4_half", "stars_4", "stars_3_half", "stars_3", "stars_2_half", "stars_2", "stars_1_half", "stars_1", "stars_0_half", "stars_0"
        };

        private decimal GetUserRating(string imgUrl) {
            if (!string.IsNullOrEmpty(imgUrl))
            {
                for (var i = 0; i < StarUrls.Count(); ++i)
                {
                    if (imgUrl.Contains(StarUrls[i]))
                    {
                        return 5 - (i / 2);
                    }
                }
            }
            return 0;
        
        }
    }
}
