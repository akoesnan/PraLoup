using Ninject.Modules;
using PraLoup.DataPurveyor.Attributes;
using PraLoup.DataPurveyor.Service;
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
            this.Bind<IEventService>().To<GrouponService>().WhenTargetHas<DealsAttributes>();
            this.Bind<IEventService>().To<EventfulService>().WhenTargetHas<EventsAttributes>();
            this.Bind<IEventService>().To<YelpService>().WhenTargetHas<HappyHourAttributes>();            
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