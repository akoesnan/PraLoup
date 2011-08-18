using System.Linq;
using NUnit.Framework;
using PraLoup.DataPurveyor.Client;

namespace Praloup.DataPurveyor.Test
{
    [TestFixture]
    public class GrouponTest
    {
        //[Test]
        public void TestGetGrouponDivisionSuccess()
        {
            GrouponClient g = new GrouponClient();
            var id = g.GetGrouponDivision("Seattle");
            Assert.AreEqual("seattle", id, "The division id of seattle is seattle");
        }

        //[Test]
        public void TestGetGrouponDealInSeattle()
        {
            GrouponClient g = new GrouponClient();
            var events = g.GetDataFromDivisionId("seattle");
            Assert.AreNotEqual(0, events.Count(), "there should be more than 0 events in seattle");
        }
    }
}
