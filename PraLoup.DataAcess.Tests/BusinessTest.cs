using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Testing;
using NHibernate;
using NUnit.Framework;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;
using PraLoup.DataAccess.Mapping;

namespace PraLoup.DataAccess.Tests
{
    [TestFixture]
    public class BusinessTest : BaseDataTestFixture
    {
        /// <summary>
        /// Validate that the event basic CRUD can be executed
        /// </summary>
        [Test]
        public void BusinessBasicCRUD_Success()
        {
            using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            {
                using (ISession Session = Scope.OpenSession())
                {
                    var account = EntityHelper.GetAccount("business", "owner");
                    var bu = new BusinessUser(account, Role.BusinessAdmin);
                    var op = new List<HoursOfOperation>() { new HoursOfOperation(0, DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay) };
                    new PersistenceSpecification<Business>(Session, new BusinessUsersEqualityComparer())
                   .CheckProperty(c => c.Name, "businessname")
                   .CheckProperty(c => c.Category, Category.Nightlife)
                   .CheckProperty(c => c.BusinessUsers, new List<BusinessUser>() { bu })
                   .CheckProperty(c => c.Url, "http://www.hello.com")
                   .CheckProperty(c => c.ImageUrl, "http://www.example.com/image/i.png")
                   .CheckProperty(c => c.ImageUrl, "http://www.example.com/image/i.png")
                   .CheckComponentList(c => c.HoursOfOperations, op)
                   .VerifyTheMappings();
                }
            }
        }
    }

    public class BusinessUsersEqualityComparer : IEqualityComparer
    {
        public bool Equals(object x, object y)
        {
            if (x == null || y == null)
            {
                return false;
            }
            if (x is IList<BusinessUser> && y is IList<BusinessUser>)
            {
                var t1 = (IList<BusinessUser>)x;
                var t2 = (IList<BusinessUser>)y;
                var tc = new BusinessUserEqualityComparer();
                return t1.All(t => t2.Contains(t, tc));
            }
            return x.Equals(y);
        }

        public int GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
    }

    public class BusinessUserEqualityComparer : IEqualityComparer, IEqualityComparer<BusinessUser>
    {
        public bool Equals(object x, object y)
        {
            if (x == null || y == null)
            {
                return false;
            }
            if (x is BusinessUser && y is BusinessUser)
            {
                return Equals((BusinessUser)x, (BusinessUser)y);
            }
            return x.Equals(y);
        }

        public int GetHashCode(object obj)
        {
            if (obj is BusinessUser)
            {
                return GetHashCode((BusinessUser)obj);
            }
            return obj.GetHashCode();
        }

        public bool Equals(BusinessUser x, BusinessUser y)
        {
            return x.User.Id == y.User.Id ;
        }

        public int GetHashCode(BusinessUser obj)
        {
            throw new NotImplementedException();
        }
    }
}
