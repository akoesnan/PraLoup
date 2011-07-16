using System.Collections.Generic;
using PraLoup.DataAccess.Entities;
using System.Web.Script.Serialization;
using System.Text;
using System;

namespace PraLoup.WebApp.Models
{
    public class AccountListJson
    {
        public class UserJson
        {
            public string name { get; set; }
            public string image { get; set; }
        }

        public string uniqueId{ get; set; }

        IEnumerable<Account> _accounts;
        public AccountListJson(IEnumerable<Account> list)
        {
            _accounts = list;
            uniqueId = Guid.NewGuid().ToString().Replace('-','_');
        }

        public override string ToString()
        {
            List<UserJson> l = new List<UserJson>();
            foreach (Account acc in _accounts)
            {
                UserJson uj = new UserJson();
                uj.name = acc.UserName;
                uj.image = acc.ImageUrl;
                l.Add(uj);
            }

            JavaScriptSerializer js = new JavaScriptSerializer();

            StringBuilder sb = new StringBuilder();

            js.Serialize(l, sb);

            return sb.ToString();
        }
    }
}