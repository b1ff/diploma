using System.Web.Mvc;
using Gamification.Web.Utils.CommonViewModels;

namespace Gamification.Web.Utils.ModelBinders
{
    public class FileViewModelModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var filePropertyName = bindingContext.ModelName;
            var file = controllerContext.HttpContext.Request.Files[filePropertyName];
            if (file != null && file.ContentLength > 0)
            {
                return new FileViewModel(file.FileName, file.ContentLength, file.InputStream);
            }

            return null;
        }
    }
}
