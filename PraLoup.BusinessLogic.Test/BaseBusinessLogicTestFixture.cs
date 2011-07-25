using NUnit.Framework;
using PraLoup.Infrastructure.Logging;

namespace PraLoup.BusinessLogic.Test
{
    [TestFixture]
    public abstract class BaseBusinessLogicTestFixture
    {
        protected ILogger Log = GetLogger();

        private static ILogger GetLogger()
        {
            var logger = new Log4NetLogger();
            return logger;
        }
    }
}
