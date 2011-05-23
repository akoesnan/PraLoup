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
            GrouponService g = new GrouponService();
            var id = g.GetGrouponDivision("Seattle");
            Assert.AreEqual("seattle", id, "The division id of seattle is seattle");
        }

        [TestMethod]
        public void TestGetGrouponDealInSeattle() {
            GrouponService g = new GrouponService();
            var events = g.GetDataFromDivisionId("seattle");
            Assert.AreNotEqual(0, events.Count(), "there should be more than 0 events in seattle");            
        }
    }
}
