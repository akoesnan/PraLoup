using System.Collections.Generic;
using Moq;
using NHibernate;
using NUnit.Framework;
using PraLoup.BusinessLogic.Plugins;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Enums;
using PraLoup.DataAccess.Mapping;
using PraLoup.DataAccess.Services;
using PraLoup.DataAcess.Tests;

namespace PraLoup.BusinessLogic.Test
{
    [TestFixture]
    public class ActivityActionTest
    {
        [Test]
        public void CreateActivityFromEvent_Success()
        {
            var mockDS = new Mock<IDataService>();
            IEnumerable<IActivityAction> plugins = null;
            var activityAction = new ActivityActions(mockDS.Object, plugins, new PraLoup.Infrastructure.Logging.Log4NetLogger());
            var ev = EntityHelper.GetEvent("eventName", "venueName");
            var activity = activityAction.CreateActivityFromExistingEvent(ev, Privacy.Public);

            Assert.IsNotNull(activity.Event);
            Assert.IsNotNull(activity.Organizer);
            Assert.AreEqual(Privacy.Public, activity.Privacy);
        }

        [Test]
        public void CreateActivityWithNoEvent_Success()
        {
            var mockDS = new Mock<IDataService>();
            IEnumerable<IActivityAction> plugins = null;
            var activityAction = new ActivityActions(mockDS.Object, plugins, new PraLoup.Infrastructure.Logging.Log4NetLogger());
            var ev = EntityHelper.GetEvent("eventName", "venueName");
            var activity = activityAction.CreateActivityFromNotExistingEvent(ev, Privacy.Public);

            Assert.IsNotNull(activity.Event);
            Assert.IsNotNull(activity.Organizer);
            Assert.AreEqual(Privacy.Public, activity.Privacy);
        }

        [Test]
        public void GetMyActivity_Success()
        {
            using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            {
                using (ISession Session = Scope.OpenSession())
                {

                }
            }
        }
    }
}
