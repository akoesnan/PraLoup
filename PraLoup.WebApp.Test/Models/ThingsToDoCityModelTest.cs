using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PraLoup.WebApp.Models;

namespace PraLoup.WebApp.Tests.Models
{
    [TestClass]
    public class ThingsToDoCityModelTest
    {
        [TestMethod]
        public void ConstructThingsToDoInCity_Success()
        {
            var model = new ThingsToDoCityModel("Seattle");
            model.Construct();

            Assert.AreEqual("Seattle", model.City);
            Assert.IsNotNull(model.Deals);
            Assert.IsTrue(model.Deals.Count() > 0);
            Assert.IsNotNull(model.FunEvents);
            Assert.IsTrue(model.FunEvents.Count() > 0);
            Assert.IsNull(model.HappyHours);
        }
    }
}
