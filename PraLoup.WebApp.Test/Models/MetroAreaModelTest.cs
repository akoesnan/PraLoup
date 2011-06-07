using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using PraLoup.WebApp.App_Start;
using PraLoup.WebApp.Models;
using PraLoup.DataAccess;

namespace PraLoup.WebApp.Tests.Models
{
    /// <summary>
    /// Summary description for MetroAreaModelTest
    /// </summary>
    [TestClass]
    public class MetroAreaModelTest
    {
        public MetroAreaModelTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        private static StandardKernel Kernel;

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            Kernel = new StandardKernel();
            Kernel.Load(new ThingsToDoCityModule());
            Kernel.Load(new DbEntityModule());
        }

        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ConstructorCreatedCorrectly()
        {
            var model = Kernel.Get<MetroAreaModel>();

            Assert.IsNotNull(model.Repository);
            Assert.IsNotNull(model.Repository.Context);
            Assert.IsTrue(model.Repository.Context is EntityRepository);
            Assert.IsNotNull(((EntityRepository)model.Repository.Context).DataGenerator);
        }

        [TestMethod]
        public void IsMetroSupported_SupportedCity()
        {
            var model = Kernel.Get<MetroAreaModel>();
            Assert.IsTrue(model.IsSupportedMetro("Seattle"), "Seattle should be supported");
        }

        [TestMethod]
        public void IsMetroSupported_NoSupportedCity()
        {
            var model = Kernel.Get<MetroAreaModel>();
            Assert.IsFalse(model.IsSupportedMetro("DummyCity"), "Dummy city should not be supported cities");
        }
    }
}
