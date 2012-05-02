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

        public static bool IsCurrentAction(this HttpRequestBase request, string controllerName, string actionName, object routeParams = null)
        {
            var currentState = new RequestInfo(request);

            var menuParams = new RouteValueDictionary(routeParams);
            return actionName.ToLower() == currentState.Action.ToLower()
                   && controllerName.ToLower() == currentState.Controller.ToLower()
                   && ((menuParams.Count == 0 ||
                        menuParams.All(x => currentState.RequestParams.SafeGet(x.Key) == x.Value.ToString())));
        }

        public static int? GetIntParam(this HttpRequestBase request, string paramName, bool tryGetIdAtLast = true)
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

            var getParam = request.QueryString[paramName];
            if (getParam != null && int.TryParse(getParam, out paramValue))
            {
                return paramValue;
            }

            const string idParamName = "id";
            return tryGetIdAtLast && paramName != idParamName ? request.GetIntParam(idParamName, false) : null;
        }

        public static string Controller(this HttpRequestBase request)
        {
            return request.RequestContext.RouteData.Values["controller"].SafeToString().ToLower();
        }

        public static string Area(this HttpRequestBase request)
        {
            return request.RequestContext.RouteData.DataTokens["Area"].SafeToString().ToLower();
        }

        public static bool IsCurrentArea(this HttpRequestBase request, string area)
        {
            return request.Area() == area.ToLower();
        }

        public static bool IsCurrentController(this HttpRequestBase request, string controller)
        {
            return request.Controller() == controller.ToLower();
        }
    }
}
