using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PraLoup.DataAccess.Entities;

namespace PraLoup.WebApp.Areas.Business.Models
{
    public class DealList : List<Deal>
    {
        public string[] UserGroups { get; set; }
        public void AddUserGroups(IList<UserGroup> groups)
        {
            UserGroups = new string[groups.Count + 1];
            UserGroups[0] = "Everyone";
            
            for(int i = 0; i < groups.Count; ++i)
            {
                UserGroups[i + 1] = groups[i].Name;
            }
        }
    }
}