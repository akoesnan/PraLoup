using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Testing;
using NHibernate;
using NUnit.Framework;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Mapping;
using PraLoup.DataAccess.Query;
using PraLoup.DataAccess.Services;
using PraLoup.DataAccess.Validators;
using PraLoup.Infrastructure.Data;

namespace PraLoup.DataAccess.Tests
{
    [TestFixture]
    public class AccountTest : BaseDataTestFixture
    {
        [Test]
        public void AccountBasicCRUD_Success()
        {
            using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            {
                using (ISession Session = Scope.OpenSession())
                {

                    var now = DateTime.Now;
                    new PersistenceSpecification<Account>(Session, new CustomEqualityComparer())
                   .CheckProperty(c => c.FirstName, "John")
                   .CheckProperty(c => c.LastName, "Doe")
                   .CheckProperty(c => c.FacebookLogon, new FacebookLogon() { FacebookId = 1000, AccessToken = "abc", Expires = now })
                   .CheckProperty(c => c.Email, "john@doe.com")
                   .CheckProperty(c => c.Connections, new List<Connection>() { new Connection(1, 2), new Connection(1, 3) })
                   .VerifyTheMappings();
                }
            }
        }
        Account myself, friend, friend2, friend3, friend4;
        [SetUp]
        public void Init()
        {
            myself = EntityHelper.GetAccount("mySelf", "lastName");
            friend = EntityHelper.GetAccount("friend", "lastName");
            friend2 = EntityHelper.GetAccount("friend2", "lastName");
            friend3 = EntityHelper.GetAccount("friend3", "lastName");
            friend4 = EntityHelper.GetAccount("friend4", "lastName");

            myself.Connections = new Connection[] { new Connection(myself.FacebookLogon.FacebookId, friend.FacebookLogon.FacebookId), new Connection(myself.FacebookLogon.FacebookId, friend2.FacebookLogon.FacebookId) };
            friend.Connections = new Connection[] { new Connection(friend.FacebookLogon.FacebookId, myself.FacebookLogon.FacebookId) };
            friend2.Connections = new Connection[] { new Connection(friend2.FacebookLogon.FacebookId, myself.FacebookLogon.FacebookId), new Connection(friend2.FacebookLogon.FacebookId, friend.FacebookLogon.FacebookId), new Connection(friend2.FacebookLogon.FacebookId, friend3.FacebookLogon.FacebookId) };

            friend3.Connections = new Connection[] { new Connection(friend3.FacebookLogon.FacebookId, friend2.FacebookLogon.FacebookId), new Connection(friend3.FacebookLogon.FacebookId, friend4.FacebookLogon.FacebookId) };

            friend4.Connections = new Connection[] { new Connection(friend4.FacebookLogon.FacebookId, friend3.FacebookLogon.FacebookId) };

        }

        [Test]
        public void IsFriendGenerateOkSQL_Success()
        {
            using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            {
                using (ISession Session = Scope.OpenSession())
                {
                    IRepository r = new GenericRepository(Session);
                    EntityDataService<Account, AccountValidator> ads = new EntityDataService<Account, AccountValidator>(r, new AccountValidator());

                    Session.BeginTransaction();
                    Assert.IsTrue(ads.SaveOrUpdate(myself));
                    Assert.IsTrue(ads.SaveOrUpdate(friend));
                    Assert.IsTrue(ads.SaveOrUpdate(friend2));
                    Assert.IsTrue(ads.SaveOrUpdate(friend3));
                    Assert.IsTrue(ads.SaveOrUpdate(friend4));
                    Session.Transaction.Commit();
                }

                using (ISession Session = Scope.OpenSession())
                {
                    IRepository r = new GenericRepository(Session);
                    EntityDataService<Account, AccountValidator> ads = new EntityDataService<Account, AccountValidator>(r, new AccountValidator());
                    EntityDataService<Connection, ConnectionValidator> cds = new EntityDataService<Connection, ConnectionValidator>(r, new ConnectionValidator());

                    var m = ads.Find(myself.Id);
                    var f = ads.Find(friend.Id);
                    var f3 = ads.Find(friend3.Id);
                    var f4 = ads.Find(friend4.Id);

                    // there should be 1 friend for f
                    Log.Debug("executing isfriend");
                    var spec = new AccountGetFriendQuery(f);
                    Assert.IsTrue(ads.ExecuteQuery(spec).RowCount() == 1);
                    spec = new AccountGetFriendQuery(m);
                    Assert.IsTrue(ads.ExecuteQuery(spec).RowCount() == 2);

                    // m should be friends of friend of f3
                    Log.Debug("executing isfriendoffriend");
                    var fof = new ConnectionIsFriendOfFriendQuery(m, f3);
                    Assert.IsTrue(cds.ExecuteQuery(fof).RowCount() > 0);

                    // negative case
                    fof = new ConnectionIsFriendOfFriendQuery(m, f4);
                    Assert.IsTrue(cds.ExecuteQuery(fof).RowCount() == 0);
                }
            }
        }

        internal class CustomEqualityComparer : IEqualityComparer
        {
            public bool Equals(object x, object y)
            {
                if (x == null || y == null)
                {
                    return false;
                }
                if (x is FacebookLogon && y is FacebookLogon)
                {
                    var fl1 = (FacebookLogon)x;
                    var fl2 = (FacebookLogon)y;
                    return fl1.FacebookId == fl2.FacebookId
                        && fl1.AccessToken == fl2.AccessToken
                        && fl1.Expires.Date == fl2.Expires.Date;
                }
                if (x is IList<Connection> && y is IList<Connection>)
                {
                    var s1 = (IList<Connection>)x;
                    var s2 = (IList<Connection>)y;

                    if (s1.Count != s2.Count)
                    {
                        return false;
                    }
                    else
                    {
                        if (s1.Any(s => !s2.Contains(s)))
                            return false;
                        else
                            return true;
                    }
                }
                return x.Equals(y);
            }

            public int GetHashCode(object obj)
            {
                throw new NotImplementedException();
            }
        }
    }
}
