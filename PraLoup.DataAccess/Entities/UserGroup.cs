using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.DataAccess.Entities
{
    public class UserGroup : BaseEntity
    {
        public IList<Account> Users;
        public string Name;
        public string Description;
        public Business Business;
    }
}
