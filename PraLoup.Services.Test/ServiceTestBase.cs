using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using PraLoup.WebApp.App_Start;
using PraLoup.DataAccess.Interfaces;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;

namespace PraLoup.Services.Test
{
    [TestClass]
    public class ServiceTestBase
    {
        protected static StandardKernel Kernel { get; set; }
        protected static IRepository Repository { get; set; }


        [AssemblyInitialize]
        public static void SetupTest(TestContext context)
        {
            Kernel = new StandardKernel();
            Kernel.Load(new DbEntityModule());
            Repository = Kernel.Get<IRepository>();
            Repository.Context.Database.Connection.ConnectionString = @"data source=|DataDirectory|EntityRepository.sdf";
        }

        public Account CreateTestAccount(string userName)
        {
            var a = new Account()
            {
                FirstName = "TestAccount",
                LastName = "LastName",
                UserName = userName,
                Email = "test@example.com",
                PhoneNumber = "4023046391",
                FacebookId = "123123123"
            };            
            return a;
        }

        public Event CreateTestEvent(string eventName)
        {
            var e = new Event()
            {
                Name = eventName,
                Description = "test description",
                Price = 50,
                Value = 100,
                Source = "groupon",
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddHours(10),
                ImageUrl = "http://www.example.com/image.png",
                MobileUrl = "http://m.example.com/event",
                Url = "http://www.example.com/event",
                Tags = new string[] { "tag1", "tag2" },
                Privacy = Privacy.FriendsOfFriend
            };
            return e;
        }
    }
}
