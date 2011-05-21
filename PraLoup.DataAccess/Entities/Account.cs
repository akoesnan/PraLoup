using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Enums;

namespace PraLoup.DataAccess.Entities
{
    public class Account
    {
        public int Id { get; set; }

        public string FacebookId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string TwitterId { get; set; }

        public Address Address { get; set; }
    }
}