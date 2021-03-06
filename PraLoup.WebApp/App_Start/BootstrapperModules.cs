﻿using Facebook;
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
using PraLoup.Utilities;
using DataEntities = PraLoup.DataAccess.Entities;
using ModelEntities = PraLoup.WebApp.Models.Entities;

namespace PraLoup.WebApp.App_Start
{
    public class AppModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ILogger>().To<PraLoup.Infrastructure.Logging.Log4NetLogger>().InSingletonScope();
            BindNHibernateDepedencies();
            BindFacebookActions();
            this.Bind<IFileStore>().To<DiskFileStore>();
            AutoMapCreateMap();
        }

        private void BindNHibernateDepedencies()
        {
            ILogger logger = Kernel.Get<ILogger>();
            logger.Info("Application Started");

            logger.Info("Binding NHibernate dependencies");
            NHibernateDbConfiguration nhUtility = new NHibernateDbConfiguration();
            this.Bind<ISessionFactory>().ToConstant(nhUtility.SessionFactory).InSingletonScope();
            this.Bind<IDataService>().To<DataService>().InRequestScope();
            this.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            this.Bind<ISession>().ToProvider(new SessionProvider()).InRequestScope();
            this.Bind<IRepository>().To<GenericRepository>();
        }

        private void BindFacebookActions()
        {
            // logger.Info("Binding Facebook Action");
            this.Bind<IEventAction>().To<FacebookEventActions>();
            this.Bind<FacebookClient>().To<FacebookClient>().InRequestScope();
            this.Bind<AccountBase>().To<AccountBase>().InRequestScope();
        }

        private void AutoMapCreateMap()
        {
            AutoMapper.Mapper.CreateMap<DataEntities.Business, ModelEntities.Business>();
            AutoMapper.Mapper.CreateMap<ModelEntities.Business, DataEntities.Business>();
            AutoMapper.Mapper.CreateMap<DataEntities.Promotion, ModelEntities.Promotion>();
            AutoMapper.Mapper.CreateMap<ModelEntities.Promotion, DataEntities.Promotion>();
            AutoMapper.Mapper.CreateMap<DataEntities.Deal, ModelEntities.Deal>();
            AutoMapper.Mapper.CreateMap<ModelEntities.Deal, DataEntities.Deal>();
            AutoMapper.Mapper.CreateMap<DataEntities.Address, ModelEntities.Address>();
            AutoMapper.Mapper.CreateMap<ModelEntities.Address, DataEntities.Address>();
            AutoMapper.Mapper.CreateMap<DataEntities.Event, ModelEntities.Event>();
            AutoMapper.Mapper.CreateMap<ModelEntities.Event, DataEntities.Event>();
            AutoMapper.Mapper.CreateMap<DataEntities.Venue, ModelEntities.Venue>();
            AutoMapper.Mapper.CreateMap<ModelEntities.Venue, DataEntities.Venue>();
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
            var unitOfWork = context.Kernel.Get<ISessionFactory>();
            return unitOfWork.OpenSession();
        }
    }
}
