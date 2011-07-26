using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;
using PraLoup.DataAccess.Mapping;
using PraLoup.DataAccess.Services;
using PraLoup.DataAccess.Validators;
using PraLoup.DataAccess.Tests;
using PraLoup.Infrastructure.Data;

namespace PraLoup.BusinessLogic.Test
{
    [TestClass]
    public class PromotionInstanceActionsTest : BaseBusinessLogicTestFixture
    {
        [TestMethod]
        public void TestCreatePromotionInstance()
        {
            var a = EntityHelper.GetAccount("first", "lastName");
            var i1 = EntityHelper.GetAccount("invite1", "lastName");
            var i2 = EntityHelper.GetAccount("invite2", "lastName");
            var b = EntityHelper.GetBusiness("business1", Category.ActiveLife);
            var e = EntityHelper.GetEvent("ev name", "venue name");
            var d = EntityHelper.GetDeal("dealname", 10);
            var p = new Promotion(b, e, d, 100, 5);

            using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            {
                using (ISession Session = Scope.OpenSession())
                {
                    IRepository r = new GenericRepository(Session);
                    EntityDataService<Business, BusinessValidator> bds = new EntityDataService<Business, BusinessValidator>(r, new BusinessValidator());
                    EntityDataService<Promotion, PromotionValidator> pds = new EntityDataService<Promotion, PromotionValidator>(r, new PromotionValidator());

                    IDataService ds = new DataService(null, bds, pds, null, null, null, null, new UnitOfWork(Scope.GetSessionFactory()));
                    BusinessActions ba = new BusinessActions(a, ds, Log, null);
                    PromotionActions pa = new PromotionActions(a, ds, Log, null);

                    Session.Transaction.Begin();
                    b = ba.CreateBusiness(b, Role.BusinessAdmin);
                    Assert.IsNotNull(b, "business should be saved succesfully");
                    Session.Transaction.Commit();
                }

                using (ISession Session = Scope.OpenSession())
                {
                    IRepository r = new GenericRepository(Session);
                    EntityDataService<Business, BusinessValidator> bds = new EntityDataService<Business, BusinessValidator>(r, new BusinessValidator());
                    EntityDataService<Promotion, PromotionValidator> pds = new EntityDataService<Promotion, PromotionValidator>(r, new PromotionValidator());

                    IDataService ds = new DataService(null, bds, pds, null, null, null, null, new UnitOfWork(Scope.GetSessionFactory()));
                    BusinessActions ba = new BusinessActions(a, ds, Log, null);
                    PromotionActions pa = new PromotionActions(a, ds, Log, null);

                    Session.Transaction.Begin();
                    p = pa.SavePromotion(p);
                    Assert.IsNotNull(p, "promotion should be saved succesfully");

                    Session.Transaction.Commit();
                }
                IEnumerable<PromotionInstance> promoInstances;
                using (ISession Session = Scope.OpenSession())
                {
                    IRepository r = new GenericRepository(Session);
                    var bds = new EntityDataService<Business, BusinessValidator>(r, new BusinessValidator());
                    var pds = new EntityDataService<Promotion, PromotionValidator>(r, new PromotionValidator());
                    var pids = new EntityDataService<PromotionInstance, PromotionInstanceValidator>(r, new PromotionInstanceValidator());

                    IDataService ds = new DataService(null, bds, pds, null, pids, null, null, new UnitOfWork(Scope.GetSessionFactory()));
                    BusinessActions ba = new BusinessActions(a, ds, Log, null);
                    PromotionActions pa = new PromotionActions(a, ds, Log, null);
                    PromotionInstanceActions pia = new PromotionInstanceActions(a, ds, Log, null);

                    Session.Transaction.Begin();
                    promoInstances = pia.CreatePromoInstance(p, new Account[] { i1, i2 }, "going here");
                    Assert.IsNotNull(promoInstances, "promotion should be saved succesfully");
                    Assert.AreEqual(2, promoInstances.Count(), "we should get 2 promotion instance");

                    Session.Transaction.Commit();
                }
            }
        }

        [TestMethod]
        public void TestAcceptDeclinePromotionInstance()
        {
            var a = EntityHelper.GetAccount("first", "lastName");
            var i1 = EntityHelper.GetAccount("invite1", "lastName");
            var i2 = EntityHelper.GetAccount("invite2", "lastName");
            var i3 = EntityHelper.GetAccount("invite3", "lastName");
            var b = EntityHelper.GetBusiness("business1", Category.ActiveLife);
            var e = EntityHelper.GetEvent("ev name", "venue name");
            var d = EntityHelper.GetDeal("dealname", 10);
            var p = new Promotion(b, e, d, 100, 5);

            using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            {
                using (ISession Session = Scope.OpenSession())
                {
                    IRepository r = new GenericRepository(Session);
                    EntityDataService<Business, BusinessValidator> bds = new EntityDataService<Business, BusinessValidator>(r, new BusinessValidator());
                    EntityDataService<Promotion, PromotionValidator> pds = new EntityDataService<Promotion, PromotionValidator>(r, new PromotionValidator());

                    IDataService ds = new DataService(null, bds, pds, null, null, null, null, new UnitOfWork(Scope.GetSessionFactory()));
                    BusinessActions ba = new BusinessActions(a, ds, Log, null);
                    PromotionActions pa = new PromotionActions(a, ds, Log, null);

                    Session.Transaction.Begin();
                    b = ba.CreateBusiness(b, Role.BusinessAdmin);
                    Assert.IsNotNull(b, "business should be saved succesfully");
                    Session.Transaction.Commit();
                }

                using (ISession Session = Scope.OpenSession())
                {
                    IRepository r = new GenericRepository(Session);
                    EntityDataService<Business, BusinessValidator> bds = new EntityDataService<Business, BusinessValidator>(r, new BusinessValidator());
                    EntityDataService<Promotion, PromotionValidator> pds = new EntityDataService<Promotion, PromotionValidator>(r, new PromotionValidator());

                    IDataService ds = new DataService(null, bds, pds, null, null, null, null, new UnitOfWork(Scope.GetSessionFactory()));
                    BusinessActions ba = new BusinessActions(a, ds, Log, null);
                    PromotionActions pa = new PromotionActions(a, ds, Log, null);

                    Session.Transaction.Begin();
                    p = pa.SavePromotion(p);
                    Assert.IsNotNull(p, "promotion should be saved succesfully");

                    Session.Transaction.Commit();
                }
                IEnumerable<PromotionInstance> promoInstances;
                using (ISession Session = Scope.OpenSession())
                {
                    IRepository r = new GenericRepository(Session);
                    var bds = new EntityDataService<Business, BusinessValidator>(r, new BusinessValidator());
                    var pds = new EntityDataService<Promotion, PromotionValidator>(r, new PromotionValidator());
                    var pids = new EntityDataService<PromotionInstance, PromotionInstanceValidator>(r, new PromotionInstanceValidator());

                    IDataService ds = new DataService(null, bds, pds, null, pids, null, null, new UnitOfWork(Scope.GetSessionFactory()));
                    BusinessActions ba = new BusinessActions(a, ds, Log, null);
                    PromotionActions pa = new PromotionActions(a, ds, Log, null);
                    PromotionInstanceActions pia = new PromotionInstanceActions(a, ds, Log, null);

                    Session.Transaction.Begin();
                    promoInstances = pia.CreatePromoInstance(p, new Account[] { i1, i2, i3 }, "going here");
                    Assert.IsNotNull(promoInstances, "promotion should be saved succesfully");
                    Assert.AreEqual(3, promoInstances.Count(), "we should get 2 promotion instance");

                    Session.Transaction.Commit();
                }

                using (ISession Session = Scope.OpenSession())
                {
                    IRepository r = new GenericRepository(Session);
                    var bds = new EntityDataService<Business, BusinessValidator>(r, new BusinessValidator());
                    var pds = new EntityDataService<Promotion, PromotionValidator>(r, new PromotionValidator());
                    var pids = new EntityDataService<PromotionInstance, PromotionInstanceValidator>(r, new PromotionInstanceValidator());

                    IDataService ds = new DataService(null, bds, pds, null, pids, null, null, new UnitOfWork(Scope.GetSessionFactory()));
                    PromotionInstanceActions pia = new PromotionInstanceActions(i1, ds, Log, null);

                    Session.Transaction.Begin();
                    var pia1 = pia.Accept(promoInstances.First(), "this sound like fun");

                    Assert.IsNotNull(pia1);
                    Assert.AreEqual(StatusType.Accept, pia1.Status.StatusType);
                    Assert.IsNotNull(pia1.Coupons, "Coupoin should not be null");
                    Assert.IsNotNull(pia1.Coupons.First().CouponCode, "Coupon code should be generated");
                    Session.Transaction.Commit();
                }

                using (ISession Session = Scope.OpenSession())
                {
                    IRepository r = new GenericRepository(Session);
                    var bds = new EntityDataService<Business, BusinessValidator>(r, new BusinessValidator());
                    var pds = new EntityDataService<Promotion, PromotionValidator>(r, new PromotionValidator());
                    var pids = new EntityDataService<PromotionInstance, PromotionInstanceValidator>(r, new PromotionInstanceValidator());

                    IDataService ds = new DataService(null, bds, pds, null, pids, null, null, new UnitOfWork(Scope.GetSessionFactory()));
                    PromotionInstanceActions pia = new PromotionInstanceActions(i2, ds, Log, null);

                    Session.Transaction.Begin();
                    var pia2 = pia.Decline(promoInstances.ElementAt(1), "this sound like fun");

                    Assert.IsNotNull(pia2);
                    Assert.AreEqual(StatusType.Decline, pia2.Status.StatusType);
                    Assert.IsNull(pia2.Coupons, "Coupoin should not be null");
                    Session.Transaction.Commit();
                }
            }
        }

        [TestMethod]
        public void TestForwardPromotionInstance()
        {
            var a = EntityHelper.GetAccount("first", "lastName");
            var i1 = EntityHelper.GetAccount("invite1", "lastName");
            var i2 = EntityHelper.GetAccount("invite2", "lastName");
            var i3 = EntityHelper.GetAccount("invite3", "lastName");

            var i4 = EntityHelper.GetAccount("invite4", "lastName");
            var i5 = EntityHelper.GetAccount("invite5", "lastName");
            var i6 = EntityHelper.GetAccount("invite6", "lastName");

            var b = EntityHelper.GetBusiness("business1", Category.ActiveLife);
            var e = EntityHelper.GetEvent("ev name", "venue name");
            var d = EntityHelper.GetDeal("dealname", 10);
            var p = new Promotion(b, e, d, 100, 5);

            using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            {
                using (ISession Session = Scope.OpenSession())
                {
                    IRepository r = new GenericRepository(Session);
                    EntityDataService<Business, BusinessValidator> bds = new EntityDataService<Business, BusinessValidator>(r, new BusinessValidator());
                    EntityDataService<Promotion, PromotionValidator> pds = new EntityDataService<Promotion, PromotionValidator>(r, new PromotionValidator());

                    IDataService ds = new DataService(null, bds, pds, null, null, null, null, new UnitOfWork(Scope.GetSessionFactory()));
                    BusinessActions ba = new BusinessActions(a, ds, Log, null);
                    PromotionActions pa = new PromotionActions(a, ds, Log, null);

                    Session.Transaction.Begin();
                    b = ba.CreateBusiness(b, Role.BusinessAdmin);
                    Assert.IsNotNull(b, "business should be saved succesfully");
                    Session.Transaction.Commit();
                }

                using (ISession Session = Scope.OpenSession())
                {
                    IRepository r = new GenericRepository(Session);
                    EntityDataService<Business, BusinessValidator> bds = new EntityDataService<Business, BusinessValidator>(r, new BusinessValidator());
                    EntityDataService<Promotion, PromotionValidator> pds = new EntityDataService<Promotion, PromotionValidator>(r, new PromotionValidator());

                    IDataService ds = new DataService(null, bds, pds, null, null, null, null, new UnitOfWork(Scope.GetSessionFactory()));
                    BusinessActions ba = new BusinessActions(a, ds, Log, null);
                    PromotionActions pa = new PromotionActions(a, ds, Log, null);

                    Session.Transaction.Begin();
                    p = pa.SavePromotion(p);
                    Assert.IsNotNull(p, "promotion should be saved succesfully");

                    Session.Transaction.Commit();
                }
                IEnumerable<PromotionInstance> promoInstances;
                using (ISession Session = Scope.OpenSession())
                {
                    IRepository r = new GenericRepository(Session);
                    var bds = new EntityDataService<Business, BusinessValidator>(r, new BusinessValidator());
                    var pds = new EntityDataService<Promotion, PromotionValidator>(r, new PromotionValidator());
                    var pids = new EntityDataService<PromotionInstance, PromotionInstanceValidator>(r, new PromotionInstanceValidator());

                    IDataService ds = new DataService(null, bds, pds, null, pids, null, null, new UnitOfWork(Scope.GetSessionFactory()));
                    BusinessActions ba = new BusinessActions(a, ds, Log, null);
                    PromotionActions pa = new PromotionActions(a, ds, Log, null);
                    PromotionInstanceActions pia = new PromotionInstanceActions(a, ds, Log, null);

                    Session.Transaction.Begin();
                    promoInstances = pia.CreatePromoInstance(p, new Account[] { i1, i2, i3 }, "going here");
                    Assert.IsNotNull(promoInstances, "promotion should be saved succesfully");
                    Assert.AreEqual(3, promoInstances.Count(), "we should get 2 promotion instance");

                    Session.Transaction.Commit();
                }

                using (ISession Session = Scope.OpenSession())
                {
                    IRepository r = new GenericRepository(Session);
                    var bds = new EntityDataService<Business, BusinessValidator>(r, new BusinessValidator());
                    var pds = new EntityDataService<Promotion, PromotionValidator>(r, new PromotionValidator());
                    var pids = new EntityDataService<PromotionInstance, PromotionInstanceValidator>(r, new PromotionInstanceValidator());

                    IDataService ds = new DataService(null, bds, pds, null, pids, null, null, new UnitOfWork(Scope.GetSessionFactory()));
                    PromotionInstanceActions pia = new PromotionInstanceActions(i1, ds, Log, null);

                    Session.Transaction.Begin();
                    var pia1 = pia.Accept(promoInstances.First(), "this sound like fun");

                    Assert.IsNotNull(pia1);
                    Assert.AreEqual(StatusType.Accept, pia1.Status.StatusType);
                    Assert.IsNotNull(pia1.Coupons, "Coupoin should not be null");
                    Assert.IsNotNull(pia1.Coupons.First().CouponCode, "Coupon code should be generated");
                    Session.Transaction.Commit();
                }

                using (ISession Session = Scope.OpenSession())
                {
                    IRepository r = new GenericRepository(Session);
                    var bds = new EntityDataService<Business, BusinessValidator>(r, new BusinessValidator());
                    var pds = new EntityDataService<Promotion, PromotionValidator>(r, new PromotionValidator());
                    var pids = new EntityDataService<PromotionInstance, PromotionInstanceValidator>(r, new PromotionInstanceValidator());

                    IDataService ds = new DataService(null, bds, pds, null, pids, null, null, new UnitOfWork(Scope.GetSessionFactory()));
                    PromotionInstanceActions pia = new PromotionInstanceActions(i2, ds, Log, null);

                    Session.Transaction.Begin();
                    var pia2 = pia.Decline(promoInstances.ElementAt(1), "this sound like fun");

                    Assert.IsNotNull(pia2);
                    Assert.AreEqual(StatusType.Decline, pia2.Status.StatusType);
                    Assert.IsNull(pia2.Coupons, "Coupoin should not be null");
                    Session.Transaction.Commit();
                }

                using (ISession Session = Scope.OpenSession())
                {
                    IRepository r = new GenericRepository(Session);
                    var bds = new EntityDataService<Business, BusinessValidator>(r, new BusinessValidator());
                    var pds = new EntityDataService<Promotion, PromotionValidator>(r, new PromotionValidator());
                    var pids = new EntityDataService<PromotionInstance, PromotionInstanceValidator>(r, new PromotionInstanceValidator());


                    IDataService ds = new DataService(null, bds, pds, null, pids, null, null, new UnitOfWork(Scope.GetSessionFactory()));
                    PromotionInstanceActions pia = new PromotionInstanceActions(i2, ds, Log, null);
                    Session.Transaction.Begin();

                    var forwards = pia.Forward(promoInstances.First(), new Account[] { i4, i5, i6 }, "message");

                    Session.Transaction.Commit();
                }
            }
        }
    }
}
