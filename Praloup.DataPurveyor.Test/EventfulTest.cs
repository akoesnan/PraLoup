using System.Linq;
using NUnit.Framework;
using PraLoup.DataPurveyor.Client;

namespace Praloup.DataPurveyor.Test
{
    [TestFixture]
    public class EventfulTest
    {
        //[Test]
        public void EventfulFetchData_Success()
        {
            var eventful = new EventfulClient();
            var events = eventful.GetEventData("seattle");
            Assert.IsNotNull(events, "events should not be null");
            Assert.AreNotEqual(0, events.Count(), "There should be more than 0 events in seattle");
        }
    }
}
