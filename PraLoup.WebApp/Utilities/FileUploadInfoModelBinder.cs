using System.Collections.Generic;
using System.Web.Mvc;
using PraLoup.Utilities;

namespace PraLoup.WebApp.Utilities
{
    public class UploadedFileInfoArrayBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var files = controllerContext.HttpContext.Request.Files;
            var list = new List<FileUploadInfo>();

            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                var name = files.AllKeys[i];
                var fileInfo = new FileUploadInfo(name, file);
                list.Add(fileInfo);
            }

            return list.ToArray();
        }
    }
}
