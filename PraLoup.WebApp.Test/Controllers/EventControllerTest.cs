using Ninject;
using NUnit.Framework;
using PraLoup.WebApp.App_Start;
using PraLoup.WebApp.Controllers;

namespace PraLoup.WebApp.Tests.Controllers
{
    [TestFixture]
    public class EventControllerTest
    {
        [Test]
        public void ConstructorCreatedCorrectly()
        {
            var kernel = new StandardKernel();
            kernel.Load(new ThingsToDoCityModule());
            kernel.Load(new AppModule());

            var controller = kernel.Get<EventController>();
        }
    }
}
