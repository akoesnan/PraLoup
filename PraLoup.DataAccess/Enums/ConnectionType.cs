
using System;
namespace PraLoup.DataAccess.Enums
{
    [Flags]
    public enum ConnectionType
    {
        NoConnection = 0,
        Owner = 1,
        Invited = 2,
        Friend = 4,
        FriendOfFriend = 8,
        Fans = 16
    }
}