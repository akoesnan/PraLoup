using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using PraLoup.WebApp.App_Start;
using PraLoup.WebApp.Controllers;

namespace PraLoup.WebApp.Tests.Controllers
{
    [TestClass]
    public class ThingsToDoControllerTest
    {
        [TestMethod]
        public void ConstructorCreatedCorrectly()
        {
            var kernel = new StandardKernel();
            kernel.Load(new ThingsToDoCityModule());
            kernel.Load(new AppModule());

            var controller = kernel.Get<ThingsToDoController>();
            Assert.IsNotNull(controller.MetroAreaModel);
            Assert.IsNotNull(controller.ThingsToDoCityModel);
        }
    }
}
