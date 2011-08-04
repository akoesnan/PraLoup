
using System.Linq;
using NUnit.Framework;
using PraLoup.DataAccess.Entities;
using PraLoup.DataPurveyor.Client;

namespace Praloup.DataPurveyor.Test
{
    [TestFixture]
    public class YelpTest
    {
        //[Test]
        public void TestYelpService_ReturnEvents()
        {
            var service = new YelpClient();
            var events = service.GetEventData("Settle");
            Assert.IsNotNull(events);
            Assert.IsTrue(events.Count() > 5, "Events from Yelp.com should be more than 0");
            foreach (var e in events)
            {
                AssertEventPopulated(e);
            }
        }

        private void AssertEventPopulated(Event e)
        {
            Assert.IsTrue(!string.IsNullOrEmpty(e.Name), "name should not be empty");
            Assert.IsTrue(!string.IsNullOrEmpty(e.ImageUrl), "imageurl should not be empty");
            Assert.IsTrue(e.UserReviewsCount > 0, "review count should be > 0");
            Assert.IsTrue(e.UserRating > 0, "rating should be > 0");
        }
    }
}
