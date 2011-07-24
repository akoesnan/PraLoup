using PraLoup.DataAccess.Enums;
namespace PraLoup.DataAccess.Entities
{
    public class BusinessUser : BaseEntity
    {
        public virtual Account User { get; set; }
        public virtual Role Role { get; set; }
    }
}
