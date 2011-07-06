using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PraLoup.DataAccess.Entities
{
    public class FacebookUser
    {
        [Key]
        public long FacebookId { get; set; }
        public string AccessToken { get; set; }
        public DateTime Expires { get; set; }
        public string Name { get; set; }
    }
}
