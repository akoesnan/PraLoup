using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.DataAccess.Entities
{
    public class UserGroup : BaseEntity
    {
        public virtual IList<Account> Users { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual Business Business { get; set; }
    }
}
