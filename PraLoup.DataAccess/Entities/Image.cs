using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.DataAccess.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime CreateDateTime { get; set; }
        public IEnumerable<Comment> Comments {get;set;}
    }
}
