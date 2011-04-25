using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Enums;


namespace PraLoup.DataAccess.Entities
{
    public class Venue : Address
    {
        public string Name { get; set; }
    }
}
