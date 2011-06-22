using System.Collections.Generic;
using PraLoup.DataAccess.Enums;

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

        public Privacy Privacy { get; set; }

    }
}