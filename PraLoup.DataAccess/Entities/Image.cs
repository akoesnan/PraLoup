using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.DataAccess.Entities
{
    public class Image : BaseEntity
    {
        public virtual string Url { get; set; }
        public virtual string Description { get; set; }
        public virtual IList<Comment> Comments { get; set; }
    }
}
