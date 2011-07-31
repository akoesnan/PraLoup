using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
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
    [TestClass]
    public class PromotionActionsTest : BaseBusinessLogicTestFixture
    {
        [TestMethod]
        public void TestCreatePromotion()
        {
            var a = EntityHelper.GetAccount("first", "lastName");
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

                    IDataService ds = new DataService(null, bds, pds, null, null, null, null, null, null, new UnitOfWork(Scope.GetSessionFactory().OpenSession()));
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

                    IDataService ds = new DataService(null, bds, pds, null, null, null, null, null, null, new UnitOfWork(Scope.GetSessionFactory().OpenSession()));
                    BusinessActions ba = new BusinessActions(a, ds, Log, null);
                    PromotionActions pa = new PromotionActions(a, ds, Log, null);

                    Session.Transaction.Begin();
                    p = pa.SavePromotion(p);
                    Assert.IsNotNull(p, "promotion should be saved succesfully");

                    Session.Transaction.Commit();
                }

                using (ISession Session = Scope.OpenSession())
                {
                    IRepository r = new GenericRepository(Session);
                    EntityDataService<Promotion, PromotionValidator> pds = new EntityDataService<Promotion, PromotionValidator>(r, new PromotionValidator());

                    var p1 = pds.Find(p.Id);
                    Assert.IsNotNull(p1);
                    Assert.AreEqual(p, p1);
                }
            }
        }
    }
}
