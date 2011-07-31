using System.Collections.Generic;

namespace PraLoup.WebApp.Models.Entities
{
    public class UserGroup
    {
        public virtual IList<Account> Users { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual Business Business { get; set; }
    }
}
