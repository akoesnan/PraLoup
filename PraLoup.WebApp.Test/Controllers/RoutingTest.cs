using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using PraLoup.WebApp.Areas.Admin;
using PraLoup.WebApp.Areas.Business;

namespace PraLoup.WebApp.Tests.Controllers
{
    [TestFixture]
    public class RoutingTest
    {
        [Test]
        public void ForwardSlashGoToHomeIndex()
        {
            TestIncomingRoute("~/", new Dictionary<string, string> { { "controller", "Home" }, { "action", "Index" } });
        }

        [Test]
        public void ThingsToDoCityRouteTest()
        {
            TestIncomingRoute("~/ThingsToDo/Seattle", new Dictionary<string, string> { { "controller", "ThingsToDo" }, { "action", "City" }, { "city", "Seattle" } });
        }

        [Test]
        public void AdminHomeRouteTest()
        {
            TestIncomingRoute("~/Admin", new Dictionary<string, string> { { "controller", "Home" }, { "action", "Index" } });
        }

        [Test]
        public void BusinessHomeRouteTest()
        {
            TestIncomingRoute("~/Business", new Dictionary<string, string> { { "controller", "Business" }, { "action", "Index" } });
        }

        [Test]
        public void BusinessCreateRouteTest()
        {
            TestIncomingRoute("~/Business/Create", new Dictionary<string, string> { { "controller", "Business" }, { "action", "Create" } });
        }

        [Test]
        public void BusinessCreatePromotionRouteTest()
        {
            TestIncomingRoute("~/Business/Promotion/Create", new Dictionary<string, string> { { "controller", "Promotion" }, { "action", "Create" } });
        }

        [Test]
        public void AdminCreateBusinessRouteTest()
        {
            TestIncomingRoute("~/Admin/Business/Index", new Dictionary<string, string> { { "controller", "Business" }, { "action", "Index" } });
        }

        [Test]
        public void ThingsToDoUrlGenerationTest()
        {
            var url = GenerateUrlFromMock(new { controller = "ThingsToDo", action = "City", city = "Seattle" });
            Assert.AreEqual("/ThingsToDo/Seattle", url);
        }

        private void TestIncomingRoute(string url, Dictionary<string, string> route)
        {
            RouteCollection routes = new RouteCollection();
            var adminArea = new AdminAreaRegistration();
            adminArea.RegisterArea(new AreaRegistrationContext(adminArea.AreaName, routes));
            MvcApplication.RegisterRoutes(routes);

            var httpContext = GetMockHttpContext(url);

            var routeData = routes.GetRouteData(httpContext.Object);

            Assert.IsNotNull(routeData, "route data should not be null");
            Assert.IsNotNull(routeData.Route, "route should not be null");

            foreach (var key in route.Keys)
            {
                if (route[key] == null)
                {
                    Assert.IsNull(routeData.Values[key]);
                }
                else
                {
                    Assert.AreEqual(route[key], routeData.Values[key], "unexptected value for {0}", key);
                }
            }
        }

        private static Mock<HttpContextBase> GetMockHttpContext(string url)
        {
            var httpContext = new Mock<HttpContextBase>();

            var req = new Mock<HttpRequestBase>();
            req.Setup(x => x.AppRelativeCurrentExecutionFilePath).Returns(url);

            httpContext.Setup(x => x.Request).Returns(req.Object);

            var res = new Mock<HttpResponseBase>();
            httpContext.Setup(x => x.Response).Returns(res.Object);
            res.Setup(x => x.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(x => x);

            return httpContext;
        }

        private string GenerateUrlFromMock(object values)
        {
            var routes = new RouteCollection();
            var adminArea = new AdminAreaRegistration();
            var businessArea = new BusinessAreaRegistration();
            adminArea.RegisterArea(new AreaRegistrationContext(adminArea.AreaName, routes));
            businessArea.RegisterArea(new AreaRegistrationContext(businessArea.AreaName, routes));
            MvcApplication.RegisterRoutes(routes);

            var context = GetMockHttpContext(null);
            var reqContext = new RequestContext(context.Object, new RouteData());
            return UrlHelper.GenerateUrl(null, null, null, new RouteValueDictionary(values), routes, reqContext, true);
        }
    }
}
