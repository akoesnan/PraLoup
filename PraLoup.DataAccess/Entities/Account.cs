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
        public int Id { get; set; }

        [MaxLength(25)]
        public string UserId { get; set; }

        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [MaxLength(15)]
        [Required]        
        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string TwitterId { get; set; }

        public Address Address { get; set; }

        public string Friends { get; set; }
    }
}