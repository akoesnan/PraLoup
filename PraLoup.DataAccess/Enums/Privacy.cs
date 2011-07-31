using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PraLoup.Utilities;

namespace PraLoup.DataAccess.Enums
{
    [TypeConverter(typeof(PascalCaseWordSplittingEnumConverter))]
    public enum Privacy
    {
        [Display(Name = "Private event - only invites can see this event")]
        [FacebookValue("SECRET")]
        Private = 0,

        [Display(Name = "Only my friends can see it")]
        [FacebookValue("CLOSED")]
        Friends = 1,

        [Display(Name = "Friends of friends can see this event")]
        [FacebookValue("CLOSED")]
        FriendsOfFriend = 2,

        [Display(Name = "Public event - Everyone can see this event")]
        [FacebookValue("OPEN")]
        Public = 3
    }
}
