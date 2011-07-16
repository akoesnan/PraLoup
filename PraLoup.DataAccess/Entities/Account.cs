using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PraLoup.DataAccess;
using PraLoup.DataAccess.Enums;
using System.ComponentModel.DataAnnotations;

namespace PraLoup.DataAccess.Entities
{
    public class Account : BaseEntity
    {
        public virtual string UserId { get; set; }

        public virtual FacebookLogon FacebookLogon { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string UserName { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual string Email { get; set; }

        public virtual string TwitterId { get; set; }

        public virtual Address Address { get; set; }

        public virtual string ImageUrl { get; set; }

        // TODO: what is the representation of this friends? should we store it as array of ids instead?
        public virtual string Friends { get; set; }

        public override bool Equals(object obj)
        {
            var o = obj as Account;

            return o != null
                   && (o.Id == this.Id || o.UserId == this.UserId);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}