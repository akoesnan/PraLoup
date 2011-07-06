using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using PraLoup.Utilities;

namespace PraLoup.DataAccess.Enums
{
    [TypeConverter(typeof(PascalCaseWordSplittingEnumConverter))]
    public enum Privacy
    {
        Private = 0 ,
        Friends = 1,
        FriendsOfFriend = 2,
        Public = 3 
    }
}
