using System;
using NHibernate;
using NUnit.Framework;
using PraLoup.DataAccess.Mapping;

namespace PraLoup.DataAccess.Tests
{
    [TestFixture]
    public class PromotionInstanceTest
    {
        /// <summary>
        /// Validate that the event basic CRUD can be executed
        /// </summary>
        [Test]
        public void InvitationBasicCRUD_Success()
        {
            using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            {
                using (ISession Session = Scope.OpenSession())
                {
                    var updatedTime = DateTime.Now.Date;
                    // var organizer = GetAccount("firstname", "lastname");
                    // var ev = GetEvent("my test event", "my test venue name");

                    // new PersistenceSpecification<Invitation>(Session)
                    //.CheckProperty(i => i.Activity, 1234)
                    //     //.CheckReference(i => i.Comments, )
                    //.CheckReference(c => c.Organizer, organizer)
                    //.CheckProperty(c => c.Privacy, Privacy.Public)
                    //.CheckProperty(c => c.UpdatedTime, updatedTime)
                    //.VerifyTheMappings();
                }
            }
        }
    }
}
