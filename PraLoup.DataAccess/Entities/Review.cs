using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.DataAccess.Entities
{
    public class Review : BaseEntity
    {
        public virtual Account Author { get; set; }
        public virtual DateTime UpdatedDate { get; set; }
        public virtual string Text { get; set; }
        public virtual string Url { get; set; }
        public virtual decimal Rating { get; set; }
    }
}
