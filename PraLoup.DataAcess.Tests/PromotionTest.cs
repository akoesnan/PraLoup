using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Testing;
using NHibernate;
using NUnit.Framework;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Mapping;

namespace PraLoup.DataAccess.Tests
{
    [TestFixture]
    public class PromotionTest : BaseDataTestFixture
    {
        /// <summary>
        /// Validate that the event basic CRUD can be executed
        /// </summary>
        [Test]
        public void PromotionBasicCRUD_Success()
        {
            using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            {
                using (ISession Session = Scope.OpenSession())
                {
                    var updatedTime = DateTime.Now.Date;
                    var ev = EntityHelper.GetEvent("my test event", "my test venue name");
                    var d = EntityHelper.GetDeal("buy one get one beer", 100);
                    IList<Deal> deals = new List<Deal>() { d };
                    new PersistenceSpecification<Promotion>(Session, new PromotionEqualityComparer())
                   .CheckProperty(c => c.Event, ev)
                   .CheckProperty(c => c.Deals, deals)
                   .VerifyTheMappings();
                }
            }
        }

        public class PromotionEqualityComparer : IEqualityComparer
        {
            public bool Equals(object x, object y)
            {
                if (x == null || y == null)
                {
                    return false;
                }
                if (x is Promotion && y is Promotion)
                {
                    var t1 = (Event)x;
                    var t2 = (Event)y;
                    var tc = new EventEqualityComparer();
                    return t1.Equals(t2);
                }
                else if (x is IList<Deal> && y is IList<Deal>)
                {
                    return ((IList<Deal>)x).All(z => ((IList<Deal>)y).Contains(z));
                }
                return x.Equals(y);
            }

            public int GetHashCode(object obj)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void CreatePromotionAndQuery()
        {
            //using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            //{
            //    using (ISession Session = Scope.OpenSession())
            //    {
            //        var ev = EntityHelper.GetEvent("mytestevent", "mytestvenue");
            //        var a = EntityHelper.GetAccount("fristname", "lastname");
            //        var act = new Promotion(a, ev, Privacy.Public);

            //        var r = new GenericRepository(Session);
            //        var ads = new EntityDataService<Activity, ActivityValidator>(r, new ActivityValidator());

            //        IEnumerable<string> brokenRules;
            //        ads.SaveOrUpdate(act, out brokenRules);

            //        Assert.AreNotSame(0, ads.Find(act.Id));
            //    }
            //}
        }

        [Test]
        public void CreateActivityAndInvitationThenQuery_Success()
        {
            int activityCount = 0;
            int inviteCount = 0;
            //using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            //{
            //    using (ISession Session = Scope.OpenSession())
            //    {
            //        Session.Transaction.Begin();
            //        var ev = EntityHelper.GetEvent("mytestevent", "mytestvenue");
            //        var a = EntityHelper.GetAccount("fristname", "lastname");
            //        var b = EntityHelper.GetAccount("invitefs", "invitels");
            //        var act = new Activity(a, ev, Privacy.Public);

            //        var r = new GenericRepository(Session);
            //        var ads = new EntityDataService<Activity, ActivityValidator>(r, new ActivityValidator());
            //        var ids = new EntityDataService<PromotionInstance, InvitationValidator>(r, new InvitationValidator());

            //        IEnumerable<string> brokenRules;
            //        Assert.IsTrue(ads.SaveOrUpdate(act, out brokenRules), "we should be able to save an activity");

            //        var invite = new PromotionInstance(a, b, act, "you should go");

            //        Assert.IsTrue(ids.SaveOrUpdate(invite, out brokenRules), "should be able to save invite");

            //        Assert.AreNotSame(0, ids.Find(invite.Id));

            //        activityCount = ads.GetAll().Count();
            //        inviteCount = ids.GetAll().Count();
            //        Assert.AreNotEqual(0, activityCount);
            //        Assert.AreNotEqual(0, inviteCount);
            //        Session.Transaction.Commit();

            //        var eds = new EntityDataService<Event, EventValidator>(r, new EventValidator());
            //        var acds = new EntityDataService<Account, AccountValidator>(r, new AccountValidator());

            //        Assert.AreNotEqual(0, eds.GetAll().Count());
            //        Assert.AreNotEqual(0, acds.GetAll().Count());
            //    }
            //}
        }
    }
}
