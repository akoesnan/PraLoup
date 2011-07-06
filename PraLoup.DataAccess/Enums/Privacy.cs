using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.DataAccess.Enums
{
    public enum Privacy
    {
        [FacebookValue("SECRET")]
        Private,

        [FacebookValue("CLOSED")]
        Friends,

        [FacebookValue("CLOSED")]
        FriendsOfFriend,

        [FacebookValue("OPEN")]  
        Public
    }    
}
