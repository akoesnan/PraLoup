using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.DataAccess.Entities
{
    public class Comment : BaseEntity
    {
        public virtual DateTime CreateDateTime { get; set; }
        public virtual string Text { get; set; }
        public virtual IList<Comment> Comments { get; set; }
    }
}

