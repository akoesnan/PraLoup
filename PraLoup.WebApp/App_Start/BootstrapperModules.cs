using Ninject.Modules;
using PraLoup.DataPurveyor.Attributes;
using PraLoup.DataPurveyor.Client;
using PraLoup.DataPurveyor.Converter;
using PraLoup.DataAccess.Interfaces;
using PraLoup.DataAccess;
using System.Data.Entity;

namespace PraLoup.WebApp.App_Start
{

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

    public class DbEntityModule : NinjectModule 
    {
        public override void Load()
        {
            this.Bind<IDatabaseInitializer<EntityRepository>>().To<TestSeedDataGenerator>().InSingletonScope();
            this.Bind<DbContext>().To<EntityRepository>().WithConstructorArgument("nameOrConnectionString", "EntityRepository");
            this.Bind<EntityRepository>().To<EntityRepository>().WithConstructorArgument("nameOrConnectionString", "EntityRepository");
            this.Bind<IRepository>().To<GenericRepository>().InSingletonScope();
        }
    }
}
