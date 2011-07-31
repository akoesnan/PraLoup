using System;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace PraLoup.Utilities
{
    public class DiskFileStore : IFileStore
    {
        private string _uploadsFolder = HostingEnvironment.MapPath("~/UserContents/");

        public Guid SaveUploadedFile(HttpPostedFileBase fileBase)
        {
            var identifier = Guid.NewGuid();

            if (!Directory.Exists(_uploadsFolder))
            {
                Directory.CreateDirectory(_uploadsFolder);
            }

            var fileName = GetDiskLocation(identifier);
            if (File.Exists(fileName))
            {
                throw new Exception("File " + fileName + " already exist");
            }   

            fileBase.SaveAs(fileName);
            return identifier;
        }

        private string GetDiskLocation(Guid identifier)
        {
            return Path.Combine(_uploadsFolder, identifier.ToString());
        }
    }
}
