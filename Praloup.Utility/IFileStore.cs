using System;
using System.Web;

namespace PraLoup.Utilities
{
    public interface IFileStore
    {
        Guid SaveUploadedFile(HttpPostedFileBase fileBase);
    }
}
