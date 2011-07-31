
using System.Web;
namespace PraLoup.Utilities
{
    public class FileUploadInfo
    {
        public string Name { get; private set; }
        public HttpPostedFileBase File { get; private set; }

        public FileUploadInfo(string name, HttpPostedFileBase file)
        {
            Name = name;
            File = file;
        }
    }
}
