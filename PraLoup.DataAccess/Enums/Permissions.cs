using System;
using PraLoup.DataAccess.Entities;

namespace PraLoup.DataAccess.Enums
{
    public class Permission
    {
        BaseEntity entity;
        PermissionEnum value;

        private Permission(BaseEntity entity)
        {
            this.entity = entity;
            this.value = PermissionEnum.EmptyMask;
        }

        public bool CanEdit
        {
            get
            {
                if (entity is Event)
                    return value.HasFlag(PermissionEnum.Edit);
                else
                    return false;
            }
        }

        public bool CanDelete
        {
            get
            {
                if (entity is Event
                    || entity is PromotionInstance)
                    return value.HasFlag(PermissionEnum.Delete);
                else
                    return false;
            }
        }

        public bool CanView
        {
            get
            {
                if (entity is Event
                    || entity is Deal
                    || entity is PromotionInstance
                    || entity is PromotionInstanceStatus
                    || entity is Comment)
                    return value.HasFlag(PermissionEnum.View);
                else
                    return false;
            }
        }

        public bool CanAccept
        {
            get
            {
                if (entity is PromotionInstance)
                    return value.HasFlag(PermissionEnum.Accept);
                else
                    return false; ;
            }
        }

        public bool CanForward
        {
            get
            {
                if (entity is Promotion)
                    return value.HasFlag(PermissionEnum.Forward);
                else
                    return false;
            }
        }

        public bool CanCopy
        {
            get
            {
                if (entity is Event
                    || entity is Promotion)
                    return value.HasFlag(PermissionEnum.Copy);
                else
                    return false;
            }
        }

        public bool CanComment
        {
            get
            {
                if (entity is Event
                    || entity is Promotion)
                    return value.HasFlag(PermissionEnum.Comment);
                else
                    return false;
            }
        }

        public bool CanShare
        {
            get
            {
                if (entity is Event
                    || entity is Promotion)
                    return value.HasFlag(PermissionEnum.Share);
                else
                    return false;
            }
        }

        public static Permission GetPermissions(Promotion promo, ConnectionType connection)
        {
            var p = new Permission(promo);
            if (connection.HasFlag(ConnectionType.Owner))
            {
                p.value |= PermissionEnum.Copy;
                p.value |= PermissionEnum.Delete;
                p.value |= PermissionEnum.Edit;
                p.value |= PermissionEnum.Forward;
            }

            if (connection.HasFlag(ConnectionType.Invited))
            {
                p.value |= PermissionEnum.Accept;
                p.value |= PermissionEnum.View;
                p.value |= PermissionEnum.Copy;
            }

            // for activities, privacy means who can invite people to the activity.
            switch (promo.Event.Privacy)
            {
                case DataAccess.Enums.Privacy.Public:
                    p.value |= PermissionEnum.View;
                    p.value |= PermissionEnum.Share;
                    p.value |= PermissionEnum.Accept;
                    p.value |= PermissionEnum.Forward;
                    break;
                case DataAccess.Enums.Privacy.Friends:
                    if (connection.HasFlag(ConnectionType.Friend) && connection.HasFlag(ConnectionType.Invited))
                    {
                        p.value |= PermissionEnum.Forward;
                    }

                    break;
                case DataAccess.Enums.Privacy.FriendsOfFriend:
                    if ((connection.HasFlag(ConnectionType.FriendOfFriend) && connection.HasFlag(ConnectionType.Invited)))
                    {
                        p.value |= PermissionEnum.Forward;
                    }
                    break;
                case DataAccess.Enums.Privacy.Private:
                    break;
            }
            if (p.value != PermissionEnum.EmptyMask)
            {
                p.value |= PermissionEnum.View;
            }
            return p;
        }

        public static Permission GetPermissions(Event e, ConnectionType connection)
        {
            var p = new Permission(e);

            if (connection.HasFlag(ConnectionType.Owner))
            {
                p.value |= PermissionEnum.Copy;
                p.value |= PermissionEnum.Delete;
                p.value |= PermissionEnum.Edit;
            }

            switch (e.Privacy)
            {
                // public event can be viewed, shared and accpeted by everyone 
                case DataAccess.Enums.Privacy.Public:
                    p.value |= PermissionEnum.View;
                    p.value |= PermissionEnum.Share;
                    p.value |= PermissionEnum.Accept;
                    break;
                // friend only event can be viewed and accpeted by friends and owner only
                case DataAccess.Enums.Privacy.Friends:
                    if (connection.HasFlag(ConnectionType.Friend) || connection.HasFlag(ConnectionType.Owner))
                    {
                        p.value |= PermissionEnum.View;
                        p.value |= PermissionEnum.Accept;
                    }
                    break;
                // friend of friend can view and accept 
                case DataAccess.Enums.Privacy.FriendsOfFriend:
                    if (connection.HasFlag(ConnectionType.FriendOfFriend)
                        || connection.HasFlag(ConnectionType.Friend)
                        || connection.HasFlag(ConnectionType.Owner))
                    {
                        p.value |= PermissionEnum.View;
                        p.value |= PermissionEnum.Accept;
                    }
                    break;
                // private means that only the owner can view and and accept
                // Todo: how about people that are invited explicitly?
                case DataAccess.Enums.Privacy.Private:
                    if (connection.HasFlag(ConnectionType.Owner))
                    {
                        p.value |= PermissionEnum.View;
                        p.value |= PermissionEnum.Accept;
                    }
                    break;
            }
            if (p.value != PermissionEnum.EmptyMask)
            {
                p.value |= PermissionEnum.View;
            }
            return p;
        }

        //public static Permission GetPermissions(InvitationResponse ir, ConnectionType connection)
        //{
        //    var p = new Permission(ir);

        //    if (connection.HasFlag(ConnectionType.Owner)
        //        // connection.HasFlag(ConnectionType.ActivityOwner) -- why do we have activity owner deleting the response for?
        //        )
        //    {
        //        p.value |= PermissionEnum.Delete;
        //    }
        //    return p;
        //}

        public static Permission GetPermissions(PromotionInstance i, ConnectionType connection)
        {
            var p = new Permission(i);

            if (connection.HasFlag(ConnectionType.Owner)
                // connection.HasFlag(ConnectionType.ActivityOwner) -- why do we have activity owner deleting the response for?
                )
            {
                p.value |= PermissionEnum.Delete;
            }

            return p;

        }
    }   

    [Flags]
    internal enum PermissionEnum
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
        Forward = 32,
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
