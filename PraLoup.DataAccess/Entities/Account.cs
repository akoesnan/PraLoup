using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Enums;
using System.ComponentModel.DataAnnotations;

namespace PraLoup.DataAccess.Entities
{
    public class Account
    {   
        [Key]
        public int Id { get; set; }

        [MaxLength(25)]
        public string FacebookId { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Firstname is required")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }

        [MaxLength(15)]
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        [Required(ErrorMessage="Email is required")]
        public string Email { get; set; }

        public string TwitterId { get; set; }

        public Address Address { get; set; }
    }
}