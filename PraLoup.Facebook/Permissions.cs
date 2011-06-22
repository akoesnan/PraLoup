using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.Facebook
{
    [Flags]
    public enum Permissions
    {
        /// <summary>
        /// The empty permissions mask, ie, no permissions
        /// </summary>
        EmptyMask = 0,
        /// <summary>
        /// Permission to edit the item.
        /// Applies to Events and Activities
        /// </summary>
        Edit = 1,
        /// <summary>
        /// Permission to modify the item
        /// Applies to Events and Activities
        /// </summary>
        Modify = 2,
        /// <summary>
        /// Permission to delete the item
        /// Applies to events, activities and invitation responses
        /// </summary>
        Delete = 4,
        /// <summary>
        /// Permission to view the item
        /// Applies to events, activities, invitation responses and comments
        /// </summary>
        View = 8,
        /// <summary>
        /// Permission to accept an invitation 
        /// Applies to events, activities, 
        /// </summary>
        Accept = 16,
        /// <summary>
        /// Permission to invite guests
        /// Applies to activities
        /// </summary>
        InviteGuests = 32,
        /// <summary>
        /// Permission to copy 
        /// Applies to events and activities
        /// </summary>
        Copy = 64,
        /// <summary>
        /// Permission to add comments
        /// Applies to events and activities
        /// </summary>
        Comment = 128,
        /// <summary>
        /// Permission to share on facebook, twitter
        /// Applies to events and activities
        /// </summary>
        Share = 256,
    }


}
