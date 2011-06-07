using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.DataAccess.Entities
{
    public abstract class Post
    {
        public int Id { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
