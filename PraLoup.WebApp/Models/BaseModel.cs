using PraLoup.DataAccess.Enums;

namespace PraLoup.WebApp.Models
{
    public class BaseModel
    {
        public Permission Permission { get; private set; }

        public BaseModel(Permission permissions)
        {
            this.Permission = permissions;
        }

    }
}