
using System.ComponentModel.DataAnnotations;
namespace PraLoup.DataAccess.Enums
{
    public enum Category
    {
        [Display(Name = "Nightlife")]
        Nightlife = 1,
        [Display(Name = "Shopping")]
        Shopping = 2,
        [Display(Name = "Beauty and spas")]
        BeautyAndSpas = 3,
        [Display(Name = "Arts and entertainment")]
        ArtsAndEntertainment = 4,
        [Display(Name = "Active life")]
        ActiveLife = 5,
        [Display(Name = "Education")]
        Education = 6,
        [Display(Name = "Business")]
        Business = 7,
        [Display(Name = "Restaurant")]
        Restaurant = 8,
        [Display(Name = "Karaoke")]
        Karaoke = 9
    }
}