using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PraLoup.DataAccess.Entities
{
    /// <summary>
    /// Activity is an instance of an event that is created for a users
    /// </summary>
    public class Activity
    {
        public int ActivityId { get; set; }

        public Account Organizer { get; set; }

        public Event Event { get; set; }

        public IEnumerable<Invitation> Invites { get; set; }
    }
}