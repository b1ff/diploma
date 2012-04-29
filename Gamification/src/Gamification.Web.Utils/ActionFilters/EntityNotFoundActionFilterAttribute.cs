using System.Web;
using System.Web.Mvc;
using Gamification.Core.Exceptions;

namespace Gamification.Web.Utils.ActionFilters
{
    public class EntityNotFoundActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null && !filterContext.ExceptionHandled && filterContext.Exception is EntityNotFoundException)
            {
                filterContext.ExceptionHandled = false;
                var exception = (EntityNotFoundException)filterContext.Exception;
                throw new HttpException(404, string.Format("Not founded {0}", exception.EntityName));
            }
        }
    }
}
