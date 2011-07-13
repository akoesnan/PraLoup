﻿using System.Collections.Generic;
using PraLoup.DataAccess.Enums;
using System;

namespace PraLoup.DataAccess.Entities
{
    /// <summary>
    /// Activity is an instance of an event that is created for a particular user
    /// </summary>
    public class Activity : BaseEntity
    {
        public Activity()
        {
        }

        public Activity(Account organizer, Event evt, Privacy privacy)
        {
            this.Organizer = organizer;
            this.Event = evt;
            this.Privacy = privacy;
        }

        public virtual int FacebookId { get; set; }

        public virtual Account Organizer { get; set; }

        public virtual Privacy Privacy
        {
            get
            {
                return (Privacy)PrivacyInt;
            }
            set
            {
                PrivacyInt = (int)value;
            }
        }

        public virtual int PrivacyInt { get; set; }

        public virtual Event Event { get; set; }

        public virtual DateTime UpdatedTime { get; set; }

        public virtual bool IsCreated { get; set; }
    }
}