
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using PraLoup.BusinessLogic.Plugins;
using PraLoup.DataAccess.Services;
using PraLoup.DataAcess.Tests;
using PraLoup.Infrastructure.Logging;
using PraLoup.DataAccess;
namespace PraLoup.BusinessLogic.Test
{
    [TestFixture]
    public class InviteActionTest
    {
        [Test]
        public void SendInvitation_Success()
        {
            var mockDS = new Mock<IDataService>();
            IEnumerable<IInviteAction> plugins = null;

            var inviteAction = new InviteAction(mockDS.Object, plugins, new Log4NetLogger());
            var ev = EntityHelper.GetEvent("eventName", "venueName");
        }

        [Test]
        public void CreateActivityWithNoEvent_Success()
        {

        }
    }
}
