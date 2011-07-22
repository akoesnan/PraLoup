using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Testing;
using NHibernate;
using NUnit.Framework;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;
using PraLoup.DataAccess.Mapping;
using PraLoup.DataAccess.Services;
using PraLoup.DataAccess.Validators;

namespace PraLoup.DataAcess.Tests
{
    [TestFixture]
    public class ActivityTest : BaseDataTestFixture
    {
        /// <summary>
        /// Validate that the event basic CRUD can be executed
        /// </summary>
        [Test]
        public void ActivityBasicCRUD_NoFacebookId_Success()
        {
            using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            {
                using (ISession Session = Scope.OpenSession())
                {
                    var updatedTime = DateTime.Now.Date;
                    var organizer = EntityHelper.GetAccount("firstname", "lastname");
                    var ev = EntityHelper.GetEvent("my test event", "my test venue name");

                    new PersistenceSpecification<Activity>(Session)
                   .CheckReference(c => c.Event, ev)
                   .CheckReference(c => c.Organizer, organizer)
                   .CheckProperty(c => c.Privacy, Privacy.Public)
                   .CheckProperty(c => c.UpdatedTime, updatedTime)
                   .VerifyTheMappings();
                }
            }
        }

        /// <summary>
        /// Validate that the event basic CRUD can be executed
        /// </summary>
        [Test]
        public void ActivityBasicCRUD_Success()
        {
            using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            {
                using (ISession Session = Scope.OpenSession())
                {
                    var updatedTime = DateTime.Now.Date;
                    var organizer = EntityHelper.GetAccount("firstname", "lastname");
                    var ev = EntityHelper.GetEvent("my test event", "my test venue name");

                    new PersistenceSpecification<Activity>(Session)
                   .CheckProperty(c => c.FacebookId, 1234)
                   .CheckReference(c => c.Event, ev)
                   .CheckReference(c => c.Organizer, organizer)
                   .CheckProperty(c => c.Privacy, Privacy.Public)
                   .CheckProperty(c => c.UpdatedTime, updatedTime)
                   .VerifyTheMappings();
                }
            }
        }

        [Test]
        public void CreateActivityAndQuery()
        {
            using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            {
                using (ISession Session = Scope.OpenSession())
                {
                    var ev = EntityHelper.GetEvent("mytestevent", "mytestvenue");
                    var a = EntityHelper.GetAccount("fristname", "lastname");
                    var act = new Activity(a, ev, Privacy.Public);

                    var r = new GenericRepository(Session);
                    var ads = new EntityDataService<Activity, ActivityValidator>(r, new ActivityValidator());

                    IEnumerable<string> brokenRules;
                    ads.SaveOrUpdate(act, out brokenRules);

                    Assert.AreNotSame(0, ads.Find(act.Id));
                }
            }
        }

        [Test]
        public void CreateActivityAndInvitationThenQuery_Success()
        {
            int activityCount = 0;
            int inviteCount = 0;
            using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            {
                using (ISession Session = Scope.OpenSession())
                {
                    Session.Transaction.Begin();
                    var ev = EntityHelper.GetEvent("mytestevent", "mytestvenue");
                    var a = EntityHelper.GetAccount("fristname", "lastname");
                    var b = EntityHelper.GetAccount("invitefs", "invitels");
                    var act = new Activity(a, ev, Privacy.Public);

                    var r = new GenericRepository(Session);
                    var ads = new EntityDataService<Activity, ActivityValidator>(r, new ActivityValidator());
                    var ids = new EntityDataService<Invitation, InvitationValidator>(r, new InvitationValidator());

                    IEnumerable<string> brokenRules;
                    Assert.IsTrue(ads.SaveOrUpdate(act, out brokenRules), "we should be able to save an activity");

                    var invite = new Invitation(a, b, act, "you should go");

                    Assert.IsTrue(ids.SaveOrUpdate(invite, out brokenRules), "should be able to save invite");

                    Assert.AreNotSame(0, ids.Find(invite.Id));

                    activityCount = ads.GetAll().Count();
                    inviteCount = ids.GetAll().Count();
                    Assert.AreNotEqual(0, activityCount);
                    Assert.AreNotEqual(0, inviteCount);
                    Session.Transaction.Commit();

                    var eds = new EntityDataService<Event, EventValidator>(r, new EventValidator());
                    var acds = new EntityDataService<Account, AccountValidator>(r, new AccountValidator());

                    Assert.AreNotEqual(0, eds.GetAll().Count());
                    Assert.AreNotEqual(0, acds.GetAll().Count());
                }
            }
        }
    }
}
