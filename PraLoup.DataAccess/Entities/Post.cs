using System;
using System.Collections.Generic;

namespace PraLoup.DataAccess.Entities
{
    public abstract class Post : BaseEntity
    {
        public virtual DateTime CreateDateTime { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}
