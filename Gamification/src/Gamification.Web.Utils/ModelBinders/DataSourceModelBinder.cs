using System.Web.Mvc;
using Gamification.Core;
using Gamification.Web.Utils.CommonViewModels;

namespace Gamification.Web.Utils.ModelBinders
{
    public class DataSourceModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Guard.ArgumentNotNull(bindingContext, "bindingContext");

            var id = (int)bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ConvertTo(typeof(int));

            if (id == 0 && bindingContext.ModelMetadata.IsRequired)
            {
                AddModelRequiredError(bindingContext, controllerContext);
            }

            return CreateDataSource(id);
        }

        private static void AddModelRequiredError(ModelBindingContext bindingContext, ControllerContext controllerContext)
        {
            var errorMessage = string.Format("Field {0} is required",
                                             bindingContext.ModelMetadata.DisplayName ?? bindingContext.ModelName);

            controllerContext.Controller.ViewData.ModelState.AddModelError(bindingContext.ModelName, errorMessage);
        }

        protected virtual DataSource CreateDataSource(int id)
        {
            return new DataSource(id);
        }
    }
}
