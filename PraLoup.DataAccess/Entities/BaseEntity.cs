using System;

namespace PraLoup.DataAccess.Entities
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; protected set; }

        public override bool Equals(object obj)
        {
            var other = obj as BaseEntity;
            if (other == null)
                return false;
            if (this.GetType() != obj.GetType())
                return false;
            return Id.Equals(other.Id) && !Id.Equals(Guid.Empty);
        }
    }
}
