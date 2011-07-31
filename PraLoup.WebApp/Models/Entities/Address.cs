
using System.ComponentModel.DataAnnotations;
using PraLoup.WebApp.Resources;
using Enums = PraLoup.DataAccess.Enums;
namespace PraLoup.WebApp.Models.Entities
{
    public class Address
    {
        public Address()
        {
            Country = Enums.Country.US;
            UsState = Enums.State.WA;
        }

        [Required(ErrorMessageResourceType = typeof(LocStrings), ErrorMessageResourceName = "AddressStreet1Req")]
        [Display(ResourceType = typeof(LocStrings), Name = "AddressStreet1")]
        public string StreetLine1 { get; set; }

        [Display(ResourceType = typeof(LocStrings), Name = "AddressStreet2", Description = "AddressStreet2Desc")]
        public string StreetLine2 { get; set; }

        [Display(ResourceType = typeof(LocStrings), Name = "AddressPostalCode")]
        public string PostalCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(LocStrings), ErrorMessageResourceName = "AddressCityReq")]
        [Display(ResourceType = typeof(LocStrings), Name = "AddressCity")]
        public string City { get; set; }

        [Display(ResourceType = typeof(LocStrings), Name = "AddressState")]
        public Enums.State UsState { get; set; }

        [Display(ResourceType = typeof(LocStrings), Name = "AddressState")]
        public string NonUsState { get; set; }

        [Editable(false)]
        public string State
        {
            get
            {
                return Country == Enums.Country.US ? UsState.ToString() : NonUsState;
            }
        }

        [Required(ErrorMessageResourceType = typeof(LocStrings), ErrorMessageResourceName = "AddressCountryReq")]
        [Display(ResourceType = typeof(LocStrings), Name = "AddressCountry")]
        public Enums.Country Country { get; set; }
    }
}