using System.Collections.Generic;
using PraLoup.DataAccess.Entities;

namespace PraLoup.BusinessLogic.Plugins
{
    public interface IUserGroupAction
    {
        IList<UserGroup> GetUserGroupsForBusinessAndUser(Account user, Business business);


    }
}
