using System.Collections.Generic;
using System.Linq;
using PraLoup.DataAccess.Enums;
namespace PraLoup.DataAccess.Entities
{
    public class Business : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual Address Address { get; set; }
        public virtual Category Category { get; set; }        
        public virtual string Phone { get; set; }
        public virtual string Url { get; set; }
        public virtual FacebookLogon FacebookLogon { get; set; }
        public virtual string TwitterId { get; set; }
        public virtual IList<Connection> Connections { get; set; }
        public virtual IList<Review> Reviews { get; set; }
        public virtual IList<BusinessUser> User { get; set; }

        public virtual decimal Rating { get; set; }

        public virtual IEnumerable<long> FacebookFriendIds
        {
            get
            {
                return this.Connections.Select(c => c.FriendId);
            }
        }

    }
}
