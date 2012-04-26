using System.Linq;
using System.Web;
using System.Web.Routing;
using Gamification.Core.Extensions;

namespace Gamification.Web.Utils.Helpers
{
    public static class RequestExtensions
    {
        public static bool IsCurrentAction(
            this HttpRequest request, string controllerName, string actionName, object routeParams = null)
        {
            return new HttpRequestWrapper(request).IsCurrentAction(controllerName, actionName, routeParams);
        }

        public static bool IsCurrentAction(this HttpRequestBase request, string controllerName, string actionName, object routeParams)
        {
            var currentState = new RequestInfo(request);

            var menuParams = new RouteValueDictionary(routeParams);
            return actionName.ToLower() == currentState.Action.ToLower()
                   && controllerName.ToLower() == currentState.Controller.ToLower()
                   //&& areaName.ToLower() == currentState.Area.ToLower()
                   && ((menuParams.Count == 0 ||
                        menuParams.All(x => currentState.RequestParams.SafeGet(x.Key) == x.Value.ToString())));
        }

        public static int? GetIntParam(this HttpRequestBase request, string paramName, bool tryGetIdInTheLast = true)
        {
            var param = request.RequestContext.RouteData.Values[paramName];
            int paramValue;
            if (param != null && int.TryParse(param.SafeToString(), out paramValue))
            {
                return paramValue;
            }

            var formParam = request.Form[paramName];
            if (formParam != null && int.TryParse(formParam, out paramValue))
            {
                return paramValue;
            }

            var idParamName = "id";
            return tryGetIdInTheLast && paramName != idParamName ? request.GetIntParam(idParamName, false) : null;
        }
    }
}
