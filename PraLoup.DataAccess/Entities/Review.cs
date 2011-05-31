using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.DataAccess.Entities
{
    public class Review
    {
        public string Author {get;set;}
        public DateTime UpdatedDate {get;set;}
        public string Text {get;set;}
        public string Url {get;set;}
        public decimal Rating {get;set;}
    }
}
