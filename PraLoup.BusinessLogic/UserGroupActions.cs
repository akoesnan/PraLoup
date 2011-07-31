using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PraLoup.BusinessLogic.Plugins;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Enums;
using PraLoup.DataAccess.Query;
using PraLoup.DataAccess.Services;
using PraLoup.Infrastructure.Logging;

namespace PraLoup.BusinessLogic
{
    public class UserGroupActions : ActionBase<IUserGroupAction>
    {
        public UserGroupActions(Account account, IDataService dataService, ILogger log, IEnumerable<IUserGroupAction> actionPlugins)
            : base(account, dataService, log, actionPlugins)
        {
        }

        public IList<UserGroup> GetUserGroupsForBusinessAndUser(Account user, Business business)
        {
            var x = dataService.UserGroup.Where(t => t.Business.Id == business.Id && t.Users.Contains(user));
            if (x != null)
            {
                return x.ToList();
            }
            else
            {
                return null;
            }
        }
    }
}
