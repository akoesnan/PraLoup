using System.Collections.Generic;
using System.Web.Script.Serialization;
using NUnit.Framework;
using PraLoup.DataAccess.Entities;
using PraLoup.WebApp.Models;

namespace PraLoup.WebApp.Tests.Models
{
    [TestFixture]
    public class AccountListJsonTest
    {
        [Test]
        public void GenerateJson()
        {
            Account acc1 = new Account();
            acc1.UserName = "FOO BAR";
            acc1.ImageUrl = "FOO BAR";

            Account acc2 = new Account();
            acc2.UserName = "FOO BAR";
            acc2.ImageUrl = "FOO BAR";

            List<Account> la = new List<Account>();
            la.Add(acc1);
            la.Add(acc2);

            AccountListJson alj = new AccountListJson(la);
            string x = alj.DisplayJSON();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            IEnumerable<AccountListJson.UserJson> alj2 = jss.Deserialize<AccountListJson.UserJson[]>(x);
        }
    }
}
