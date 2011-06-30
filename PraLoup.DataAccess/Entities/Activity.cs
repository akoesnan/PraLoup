using System.Collections.Generic;
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
            Invites = new Invitations();
        }

        public virtual int ActivityId { get; set; }

        public virtual Account Organizer { get; set; }

        public Privacy Privacy
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
        public int PrivacyInt { get; set; }

        public virtual Event Event { get; set; }

        public virtual Invitations Invites { get; set; }
    }
}