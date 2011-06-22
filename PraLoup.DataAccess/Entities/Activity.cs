﻿using System.Collections.Generic;
using PraLoup.DataAccess.Enums;

namespace PraLoup.DataAccess.Entities
{
    /// <summary>
    /// Activity is an instance of an event that is created for a users
    /// </summary>
    public class Activity
    {
        public Activity()
        {
            Invites = new HashSet<Invitation>();
        }

        public virtual int ActivityId { get; set; }

        public virtual Account Organizer { get; set; }

        public Privacy Privacy { get; set; }

        public virtual Event Event { get; set; }

        public virtual ICollection<Invitation> Invites { get; set; }
    }
}