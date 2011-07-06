using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.DataAccess.Enums
{

    public enum Privacy
    {
        [FacebookValue("SECRET")]
        Private = 0,

        [FacebookValue("CLOSED")]
        Friends = 1,

        [FacebookValue("CLOSED")]
        FriendsOfFriend = 2,

        [FacebookValue("OPEN")]  
        Public = 3
    }    
}
