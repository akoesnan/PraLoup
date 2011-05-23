using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PraLoup.DataPurveyor.Service;

namespace Praloup.DataPurveyor.Test
{
    [TestClass]
    public class EventfulTest
    {
        [TestMethod]
        public void EventfulFetchData_Success()
        {
            var eventful = new EventfulService();
            var events = eventful.GetEventData("seattle");
            Assert.IsNotNull(events, "events should not be null");
            Assert.AreNotEqual(0, events.Count(), "There should be more than 0 events in seattle");
        }
    }
}
