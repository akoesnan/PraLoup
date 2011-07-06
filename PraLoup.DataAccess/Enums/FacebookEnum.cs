using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.DataAccess.Enums
{
    public class FacebookValueAttribute : Attribute
    {
        public string FacebookValue { get; set; }

        public FacebookValueAttribute(string value) {
            this.FacebookValue = value;
        }    
    }
}
