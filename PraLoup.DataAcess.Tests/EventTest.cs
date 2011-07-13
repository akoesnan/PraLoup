﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PraLoup.DataAccess.Mapping;
using PraLoup.DataAccess.Entities;
using NHibernate;
using FluentNHibernate.Testing;
using System.Collections;

namespace PraLoup.DataAcess.Tests
{
    [TestClass]
    public class EventTest : BaseDataTestClass
    {
        [TestMethod]
        public void EventBasicCRUD_Success()
        {
            using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            {
                using (ISession Session = Scope.OpenSession())
                {
                    var start = DateTime.Now.Date;
                    var end = DateTime.Now.Date.AddHours(1);

                    new PersistenceSpecification<Event>(Session, new TagsEqualityComparer())
                   .CheckProperty(c => c.Description, "Event Description")
                   .CheckProperty(c => c.Name, "Event Name")
                   .CheckProperty(c => c.ImageUrl, "http://www.example.com/image/i.png")
                   .CheckProperty(c => c.StartDateTime, start)
                   .CheckProperty(c => c.EndDateTime, end)
                   .CheckProperty(c => c.Tags, new Tag[] { new Tag("Tag1"), new Tag("Tag2") })
                   .VerifyTheMappings();
                }
            }
        }
    }

    public class TagsEqualityComparer : IEqualityComparer
    {
        public bool Equals(object x, object y)
        {
            if (x == null || y == null)
            {
                return false;
            }
            if (x is IList<Tag> && y is IList<Tag>)
            {
                var t1 = (IList<Tag>)x;
                var t2 = (IList<Tag>)y;
                var tc = new TagEqualityComparer();
                return t1.All(t => t2.Contains<Tag>(t, tc));
            }
            return x.Equals(y);
        }

        public int GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
    }

    public class TagEqualityComparer : IEqualityComparer, IEqualityComparer<Tag>
    {
        public bool Equals(object x, object y)
        {
            if (x == null || y == null)
            {
                return false;
            }
            if (x is Tag && y is Tag)
            {
                return Equals((Tag)x, (Tag)y);
            }
            return x.Equals(y);
        }

        public int GetHashCode(object obj)
        {
            if (obj is Tag)
            {
                return GetHashCode((Tag)obj);
            }
            return obj.GetHashCode();
        }

        public bool Equals(Tag x, Tag y)
        {
            return x.Text == y.Text;
        }

        public int GetHashCode(Tag obj)
        {
            throw new NotImplementedException();
        }
    }
}
