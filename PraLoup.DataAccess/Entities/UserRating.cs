
using System;
namespace PraLoup.DataAccess.Entities
{
    public class UserRating : BaseEntity
    {
        public virtual BusinessUser RatedBy { get; set; }
        public virtual byte Rate { get; set; }
        public virtual decimal? AmountSpent { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual Account User { get; set; }
    }
}
