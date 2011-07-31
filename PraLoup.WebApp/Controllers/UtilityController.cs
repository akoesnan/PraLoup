using System;
using System.Web.Mvc;
using PraLoup.Utilities;

namespace PraLoup.WebApp.Controllers
{
    public class UtilityController : Controller
    {
        private IFileStore _fileStore;
        public UtilityController(IFileStore fileStore)
        {
            this._fileStore = fileStore;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public Guid AsyncUpload(FileUploadInfo[] file)
        {
            if (file.Length > 0)
            {
                return _fileStore.SaveUploadedFile(file[0].File);
            }
            else
            {
                return Guid.Empty;
            }
        }
    }
}
