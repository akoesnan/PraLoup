﻿using NHibernate;
using NUnit.Framework;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;
using PraLoup.DataAccess.Mapping;
using PraLoup.DataAccess.Services;
using PraLoup.DataAccess.Tests;
using PraLoup.DataAccess.Validators;
using PraLoup.Infrastructure.Data;

namespace PraLoup.BusinessLogic.Test
{
    [TestFixture]
    public class BusinessActionsTest : BaseBusinessLogicTestFixture
    {
        [Test]
        public void TestCreateBusiness()
        {
            var a = EntityHelper.GetAccount("first", "lastName");
            var b = EntityHelper.GetBusiness("business1", Category.ActiveLife);

            using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            {
                using (ISession Session = Scope.OpenSession())
                {
                    Session.Transaction.Begin();
                    IRepository r = new GenericRepository(Session);
                    EntityDataService<Business, BusinessValidator> bds = new EntityDataService<Business, BusinessValidator>(r, new BusinessValidator());

                    IDataService ds = new DataService(null, bds, null, null, null, null, null, null, null, new UnitOfWork(Session));
                    BusinessActions ba = new BusinessActions(a, ds, new PraLoup.Infrastructure.Logging.Log4NetLogger(), null);
                    ba.CreateBusiness(b, a, Role.BusinessAdmin);
                    Session.Transaction.Commit();
                }

                using (ISession Session = Scope.OpenSession())
                {
                    IRepository r = new GenericRepository(Session);
                    EntityDataService<Business, BusinessValidator> bds = new EntityDataService<Business, BusinessValidator>(r, new BusinessValidator());

                    var b1 = bds.Find(b.Id);
                    Assert.IsNotNull(b1);
                    Assert.AreEqual(b, b1);
                }
            }
        }

        [Test]
        public void TestAddNewBusinessUser()
        {
            var a = EntityHelper.GetAccount("first", "lastName");
            var a2 = EntityHelper.GetAccount("first1", "lastName1");
            var b = EntityHelper.GetBusiness("business1", Category.ActiveLife);

            using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            {
                using (ISession Session = Scope.OpenSession())
                {
                    Session.Transaction.Begin();
                    IRepository r = new GenericRepository(Session);
                    EntityDataService<Business, BusinessValidator> bds = new EntityDataService<Business, BusinessValidator>(r, new BusinessValidator());

                    IDataService ds = new DataService(null, bds, null, null, null, null, null, null, null, new UnitOfWork(Session));
                    BusinessActions ba = new BusinessActions(a, ds, new PraLoup.Infrastructure.Logging.Log4NetLogger(), null);
                    ba.CreateBusiness(b, a, Role.BusinessAdmin);
                    Session.Transaction.Commit();
                }

                using (ISession Session = Scope.OpenSession())
                {
                    Session.BeginTransaction();
                    IRepository r = new GenericRepository(Session);
                    EntityDataService<Business, BusinessValidator> bds = new EntityDataService<Business, BusinessValidator>(r, new BusinessValidator());
                    IDataService ds = new DataService(null, bds, null, null, null, null, null, null, null, new UnitOfWork(Session));
                    BusinessActions ba = new BusinessActions(a, ds, new PraLoup.Infrastructure.Logging.Log4NetLogger(), null);

                    var b1 = ba.GetBusiness(b.Id);
                    ba.AddBusinessUser(b1, a2, Role.StoreStaff);
                    Session.Transaction.Commit();
                }
            }
        }

        [Test]
        public void TestUpdateBusinessUserRole()
        {
            var a = EntityHelper.GetAccount("first", "lastName");
            var a2 = EntityHelper.GetAccount("first1", "lastName1");
            var b = EntityHelper.GetBusiness("business1", Category.ActiveLife);

            using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            {
                using (ISession Session = Scope.OpenSession())
                {
                    Session.Transaction.Begin();
                    IRepository r = new GenericRepository(Session);
                    EntityDataService<Business, BusinessValidator> bds = new EntityDataService<Business, BusinessValidator>(r, new BusinessValidator());

                    IDataService ds = new DataService(null, bds, null, null, null, null, null, null, null, new UnitOfWork(Session));
                    BusinessActions ba = new BusinessActions(a, ds, new PraLoup.Infrastructure.Logging.Log4NetLogger(), null);
                    ba.CreateBusiness(b, a, Role.BusinessAdmin);
                    Session.Transaction.Commit();
                }

                using (ISession Session = Scope.OpenSession())
                {
                    IRepository r = new GenericRepository(Session);
                    EntityDataService<Business, BusinessValidator> bds = new EntityDataService<Business, BusinessValidator>(r, new BusinessValidator());
                    IDataService ds = new DataService(null, bds, null, null, null, null, null, null, null, new UnitOfWork(Session));
                    BusinessActions ba = new BusinessActions(a, ds, new PraLoup.Infrastructure.Logging.Log4NetLogger(), null);

                    Session.Transaction.Begin();
                    var b1 = ba.GetBusiness(b.Id);
                    ba.AddBusinessUser(b1, a2, Role.StoreStaff);
                    Session.Transaction.Commit();
                }

                using (ISession Session = Scope.OpenSession())
                {
                    IRepository r = new GenericRepository(Session);
                    EntityDataService<Business, BusinessValidator> bds = new EntityDataService<Business, BusinessValidator>(r, new BusinessValidator());
                    IDataService ds = new DataService(null, bds, null, null, null, null, null, null, null, new UnitOfWork(Session));
                    BusinessActions ba = new BusinessActions(a, ds, new PraLoup.Infrastructure.Logging.Log4NetLogger(), null);
                    Session.Transaction.Begin();
                    var b1 = ba.GetBusiness(b.Id);
                    ba.AddBusinessUser(b1, a2, Role.BusinessAdmin);
                    Session.Transaction.Commit();
                }
            }
        }
    }
}
