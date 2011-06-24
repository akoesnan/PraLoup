using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;

namespace PraLoup.Services.Test
{
    [TestClass]
    public class EventServiceTest : ServiceTestBase
    {
        static EventService EventService = new EventService(Repository);

        [TestMethod]
        public void Save_ValidAccount_NotExist_Success()
        {            
            var e = CreateTestEvent("test event");
            Assert.AreNotEqual(0, e.Id);
            var ne = EventService.Find(e);
            Assert.IsNotNull(ne);
            Assert.AreEqual(e, ne);
        }

        private Event CreateTestEvent(string eventName)
        {
            var e = new Event()
            {
                Name = eventName,
                Description = "test description",
                Price = 50,
                Value = 100,
                Source = "groupon",
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddHours(10),
                ImageUrl = "http://www.example.com/image.png",
                MobileUrl = "http://m.example.com/event",
                Url = "http://www.example.com/event",
                Tags = new string[] { "tag1", "tag2" },
                Privacy = Privacy.FriendsOfFriend
            };
            return e;
        }
    }
}
