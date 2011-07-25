using PraLoup.DataAccess.Enums;
namespace PraLoup.DataAccess.Entities
{
    public class BusinessUser : BaseEntity
    {
        public BusinessUser() { }

        public BusinessUser(Account user, Role role)
        {
            this.User = user;
            this.Role = role;
        }
        public virtual Account User { get; set; }
        public virtual Role Role { get; set; }
    }
}
