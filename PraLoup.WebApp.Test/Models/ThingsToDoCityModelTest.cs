using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using PraLoup.WebApp.App_Start;
using PraLoup.WebApp.Models;

namespace PraLoup.WebApp.Tests.Models
{
    [TestClass]
    public class ThingsToDoCityModelTest
    {
        [TestMethod]
        public void ConstructThingsToDoInCity_Success()
        {
            var kernel = new StandardKernel();
            kernel.Load(new ThingsToDoCityModule());

            var model = kernel.Get<ThingsToDoCityModel>();

            model.UseMultiThreads = false;
            model.Construct("Seattle");

            Assert.AreEqual("Seattle", model.City);
            Assert.IsNotNull(model.Deals);
            Assert.IsTrue(model.Deals.Count() > 0);
            Assert.IsNotNull(model.FunEvents);
            Assert.IsTrue(model.FunEvents.Count() > 0);
            Assert.IsNotNull(model.HappyHours);
            Assert.IsTrue(model.HappyHours.Count() > 0);
        }
    }
}
