using Facebook;
using NHibernate;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using PraLoup.BusinessLogic;
using PraLoup.BusinessLogic.Plugins;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Services;
using PraLoup.DataPurveyor.Attributes;
using PraLoup.DataPurveyor.Client;
using PraLoup.FacebookObjects;
using PraLoup.Infrastructure.Data;
using PraLoup.Infrastructure.Logging;

namespace PraLoup.WebApp.App_Start
{

    public class AppModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ILogger>().To<PraLoup.Infrastructure.Logging.Log4NetLogger>().InSingletonScope();

            ILogger logger = Kernel.Get<ILogger>();
            logger.Info("Application Started");

            logger.Info("Binding NHibernate dependencies");
            NHibernateDbConfiguration nhUtility = new NHibernateDbConfiguration();
            this.Bind<ISessionFactory>().ToConstant(nhUtility.SessionFactory).InSingletonScope();
            this.Bind<IDataService>().To<DataService>().InRequestScope();
            this.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            this.Bind<ISession>().ToProvider(new SessionProvider()).InRequestScope();
            this.Bind<IRepository>().To<GenericRepository>();

            // logger.Info("Binding Facebook Action");
            this.Bind<IEventAction>().To<FacebookEventActions>();
            this.Bind<FacebookClient>().To<FacebookClient>().InRequestScope();
            this.Bind<AccountBase>().To<AccountBase>().InRequestScope();
        }
    }

    /// <summary>
    /// Ninject module for things to do module
    /// This is the service type registration for 
    /// </summary>
    public class ThingsToDoCityModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IEventClient>().To<GrouponClient>().WhenTargetHas<DealsAttributes>();
            this.Bind<IEventClient>().To<EventfulClient>().WhenTargetHas<EventsAttributes>();
            this.Bind<IEventClient>().To<YelpClient>().WhenTargetHas<HappyHourAttributes>();
        }
    }

    public class SessionProvider : Provider<ISession>
    {
        protected override ISession CreateInstance(IContext context)
        {
            UnitOfWork unitOfWork = (UnitOfWork)context.Kernel.Get<IUnitOfWork>();
            return unitOfWork.Session;
        }
    }
}
