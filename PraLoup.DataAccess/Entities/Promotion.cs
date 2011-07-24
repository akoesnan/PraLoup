using System.Collections.Generic;

namespace PraLoup.DataAccess.Entities
{
    public class Promotion : BaseEntity
    {
        public virtual Event Event { get; set; }
        public virtual IList<Deal> Deals { get; set; }
        public virtual int Available { get; set; }
        public virtual int Taken { get; set; }
        public virtual byte MaxReferal { get; set; }
    }
}
