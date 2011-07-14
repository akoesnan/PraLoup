using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PraLoup.DataAccess.Entities;
using System.Data;
using NHibernate;
using FluentNHibernate.Testing;
using PraLoup.DataAccess.Mapping;
using System.Collections;

namespace PraLoup.DataAcess.Tests
{
    [TestClass]
    public class AccountTest : BaseDataTestClass
    {
        [TestMethod]
        public void AccountBasicCRUD_Success()
        {
            using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            {
                using (ISession Session = Scope.OpenSession())
                {
                    var now = DateTime.Now;
                    new PersistenceSpecification<Account>(Session, new FacebookLogonEqualityComparer())
                   .CheckProperty(c => c.Id, 1)
                   .CheckProperty(c => c.FirstName, "John")
                   .CheckProperty(c => c.LastName, "Doe")
                   .CheckProperty(c => c.FacebookLogon, new FacebookLogon() { FacebookId = 1000, AccessToken = "abc", Expires = now })
                   .CheckProperty(c => c.Email, "john@doe.com")
                   .CheckProperty(c => c.Friends, "abc")
                   .VerifyTheMappings();
                }
            }
        }

        public class FacebookLogonEqualityComparer : IEqualityComparer
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
                return x.Equals(y);
            }

            public int GetHashCode(object obj)
            {
                throw new NotImplementedException();
            }
        }
    }
}
