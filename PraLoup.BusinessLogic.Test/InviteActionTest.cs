
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using PraLoup.BusinessLogic.Plugins;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Services;
using PraLoup.Infrastructure.Logging;
namespace PraLoup.BusinessLogic.Test
{
    [TestFixture]
    public class PromotionInstanceActionTest
    {
        [Test]
        public void ForwardPromotionInstance_Success()
        {
            var mockDS = new Mock<IDataService>();
            IEnumerable<IPromotionInstanceAction> plugins = null;

            var inviteAction = new PromotionInstanceAction(mockDS.Object, plugins, new Log4NetLogger());
            var ev = EntityHelper.GetEvent("eventName", "venueName");
        }

        [Test]
        public void CreateActivityWithNoEvent_Success()
        {

        }
    }
}
