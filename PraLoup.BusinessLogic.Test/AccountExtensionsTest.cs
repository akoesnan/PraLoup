
using System.Collections.Generic;
using NHibernate;
using NUnit.Framework;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Mapping;
using PraLoup.DataAccess.Services;
using PraLoup.DataAccess.Validators;
using PraLoup.DataAccess.Tests;
using PraLoup.Infrastructure.Data;

namespace PraLoup.BusinessLogic.Test
{
    [TestFixture]
    public class AccountExtensionsTest : BaseBusinessLogicTestFixture
    {
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
        public void IsFriend_Success()
        {
            using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            {
                using (ISession Session = Scope.OpenSession())
                {
                    IRepository r = new GenericRepository(Session);
                    EntityDataService<Account, AccountValidator> ads = new EntityDataService<Account, AccountValidator>(r, new AccountValidator());
                    EntityDataService<Connection, ConnectionValidator> cds = new EntityDataService<Connection, ConnectionValidator>(r, new ConnectionValidator());
                    DataService ds = new DataService(ads, null, null, null, new UnitOfWork(Scope.GetSessionFactory()));

                    IEnumerable<string> s;
                    ds.Account.SaveOrUpdateAll(new Account[] { myself, friend, friend2, friend3, friend4 }, out s);

                    Assert.IsTrue(myself.IsFriend(friend, ds));
                    Assert.IsTrue(myself.IsFriend(friend2, ds));
                    Assert.IsTrue(friend.IsFriend(myself, ds));
                    Assert.IsTrue(friend2.IsFriend(myself, ds));
                    Assert.IsFalse(myself.IsFriend(friend3, ds));
                    Assert.IsFalse(friend3.IsFriend(myself, ds));
                }
            }
        }

        [Test]
        public void IsFriendOfFriend_Success()
        {
            using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            {
                using (ISession Session = Scope.OpenSession())
                {
                    IRepository r = new GenericRepository(Session);
                    EntityDataService<Account, AccountValidator> ads = new EntityDataService<Account, AccountValidator>(r, new AccountValidator());

                    DataService ds = new DataService(ads, null, null, null, new UnitOfWork(Scope.GetSessionFactory()));

                    IEnumerable<string> s;
                    ds.Account.SaveOrUpdateAll(new Account[] { myself, friend, friend2, friend3, friend4 }, out s);

                    Assert.IsTrue(myself.IsFriendOfFriend(friend3, ds));
                    Assert.IsTrue(friend3.IsFriendOfFriend(myself, ds));
                    Assert.IsFalse(myself.IsFriendOfFriend(friend4, ds));
                    Assert.IsFalse(friend4.IsFriendOfFriend(myself, ds));
                }
            }
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
                    var cds = new EntityDataService<Connection, ConnectionValidator>(r, new ConnectionValidator());
                    DataService ds = new DataService(ads, null, null, cds, new UnitOfWork(Scope.GetSessionFactory()));

                    IEnumerable<string> s;
                    ds.Account.SaveOrUpdateAll(new Account[] { myself, friend, friend2, friend3, friend4 }, out s);

                    var m = ads.Find(myself.Id);
                    var f = ads.Find(friend.Id);
                    var f3 = ads.Find(friend3.Id);

                    Log.Debug("executing isfriend");
                    Assert.IsTrue(m.IsFriend(f, ds));

                    Log.Debug("executing isfriendoffriend");
                    Assert.IsTrue(m.IsFriendOfFriend(f3, ds));
                }
            }
        }
    }
}
