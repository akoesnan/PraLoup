using NHibernate;
using NUnit.Framework;
using PraLoup.DataAccess.Mapping;

namespace PraLoup.DataAccess.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        [Test]
        public void CreateActivityAndQuery()
        {
            using (var Scope = new SQLiteDatabaseScope<PraLoupAutoMappingConfiguration>())
            {
                using (ISession Session = Scope.OpenSession())
                {
                    //var ev = GetEvent("mytestevent", "mytestvenue");
                    //var a = GetAccount("fristname", "lastname");
                    //var act = new Activity(a, ev, Privacy.Public);

                    //var r = new DataServ

                }
            }
        }
    }
}
