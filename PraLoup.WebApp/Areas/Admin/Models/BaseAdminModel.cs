
namespace PraLoup.WebApp.Areas.Admin.Models
{
    public class BaseAdminModel
    {
        public bool IsValid { get; protected set; }
        public bool IsAdmin { get; private set; }
    }
}