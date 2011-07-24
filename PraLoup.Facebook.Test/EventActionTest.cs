using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;
using Moq;
using PraLoup.FacebookObjects;

namespace PraLoup.Facebook.Test
{
    [TestClass]
    public class EventActionTest
    {
        [TestMethod]
        public void CreateFacebookEvent()
        {
            var e = new Event()
            {
                Description = "desc test",
                Name = "name test",
                StartDateTime = DateTime.UtcNow.AddDays(5),
                EndDateTime = DateTime.UtcNow.AddDays(6),
                Venue = new Venue()
                {
                    StreetLine1 = "Univerisity St. 10",
                    StreetLine2 = string.Empty,
                    City = "Seattle",
                    State = "Washington",
                    Country = "United States",
                    Lat = 1.123f,
                    Lon = 12.123f
                }
            };            

            Mock<IRepository> mockRepo = new Mock<IRepository>(); 
            var oauth = new OAuthHandler();
            oauth.Token = "";
            //var fbAcct = new FacebookAccount(mockRepo.Object, oauth);

            //fbAcct.CreateFacebookEvent(actv);
        }
    }
}
