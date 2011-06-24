using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PraLoup.DataAccess.Entities
{
    /// <summary>
    /// Activity is an instance of an event that is created for a particular user
    /// </summary>
    public class Activity
    {
        public Activity()
        {            
        }

        public virtual int Id { get; set; }

        public virtual Account Organizer { get; set; }

        public virtual Event Event { get; set; }
    }
}