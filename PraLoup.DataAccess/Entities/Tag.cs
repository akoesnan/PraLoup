using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.DataAccess.Entities
{
    public class Tag
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Category { get; set; }

        public string Source { get; set; }

        public bool Selected { get; set; }
    }
}
