using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PraLoup.DataAccess.Entities
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; protected set; }

        public override bool Equals(object obj)
        {
            var other = obj as BaseEntity;
            if (other == null)
                return false;
            return Id.Equals(other.Id) && !Id.Equals(Guid.Empty);
        }
    }
}
