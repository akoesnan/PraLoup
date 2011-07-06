using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PraLoup.Services.Test
{
    [TestClass]
    public class ActivityServiceTest : ServiceTestBase
    {
        static EventService EventService = new EventService(Repository);
        static ActivityService ActivityService = new ActivityService(Repository);

        [TestMethod]
        public void CreateActivityFromEvent()
        {

        }
    }
}
