using NUnit.Framework;
using PraLoup.Infrastructure.Logging;

namespace PraLoup.DataAcess.Tests
{
    [TestFixture]
    public abstract class BaseDataTestFixture
    {
        protected static readonly ILogger Log = GetLogger();

        private static ILogger GetLogger()
        {
            var logger = new Log4NetLogger();
            return logger;
        }
    }
}
