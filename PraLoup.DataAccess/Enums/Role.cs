using System.ComponentModel.DataAnnotations;
namespace PraLoup.DataAccess.Enums
{
    public enum Role
    {
        [Display(Name = "Not Affliated")]
        NoRole = 0,
        [Display(Name = "Administrator")]
        Administrator = 1,
        [Display(Name = "Business Administrator")]
        BusinessAdmin = 2,
        [Display(Name = "Business Promoter")]
        Promoter = 3,
        [Display(Name = "Door Staff")]
        DoorStaff = 4,
        [Display(Name = "Store Staff")]
        StoreStaff = 5
    }
}
