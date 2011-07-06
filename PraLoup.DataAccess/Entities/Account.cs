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
        public string UserId { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Firstname is required")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        [Required(ErrorMessage="Email is required")]
        public string Email { get; set; }

        public string TwitterId { get; set; }

        public Address Address { get; set; }

        // TODO: what is the representation of this friends? should we store it as array of ids instead?
        public string Friends { get; set; }

        public override bool Equals(object obj)
        {
            var o = obj as Account;
            
            return o != null 
                   && (o.Id == this.Id || o.UserId == this.UserId);            
        }
    }
}