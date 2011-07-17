using System.Collections.Generic;
using PraLoup.DataAccess.Entities;
using System.Web.Script.Serialization;
using System.Text;
using System;

namespace PraLoup.WebApp.Models
{
    public class AccountListJson
    {
        IEnumerable<Account> _accounts;
        public AccountListJson(IEnumerable<Account> list)
        {
            _accounts = list;
        }

        public class UserJson
        {
            public string name { get; set; }
            public string image { get; set; }
        }

        public string EditJSON()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ pre_selected_friends: [");
            bool added = false;
            foreach (var account in _accounts)
            {
                if (added)
                {
                    sb.Append(",");
                }
                sb.Append(account.FacebookLogon.FacebookId);
                added = true;
            }
            sb.Append("]}");

            return sb.ToString();
        }

        public string DisplayJSON()
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