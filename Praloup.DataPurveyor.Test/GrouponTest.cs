using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PraLoup.DataPurveyor.Service;

namespace Praloup.DataPurveyor.Test
{
    [TestClass]
    public class GrouponTest
    {
        [TestMethod]
        public void TestGetGrouponDivisionSuccess()
        {
            GrouponClient g = new GrouponClient();
            var id = g.GetGrouponDivision("Seattle");
            Assert.AreEqual("seattle", id, "The division id of seattle is seattle");
        }

        [TestMethod]
        public void TestGetGrouponDealInSeattle() {
            GrouponClient g = new GrouponClient();
            var events = g.GetDataFromDivisionId("seattle");
            Assert.AreNotEqual(0, events.Count(), "there should be more than 0 events in seattle");            
        }
    }
}
