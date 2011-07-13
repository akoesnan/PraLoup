using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PraLoup.Infrastructure.Logging;

namespace PraLoup.DataAcess.Tests
{
    [TestClass]
    public abstract class BaseDataTestClass
    {
        protected static readonly ILogger Log = GetLogger();

        private static ILogger GetLogger()
        {
            var logger = new Log4NetLogger();
            return logger;
        }
    }
}
