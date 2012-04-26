using System.Collections.Generic;
using System.Web;
using Gamification.Core.Extensions;

namespace Gamification.Web.Utils.Helpers
{
    public class RequestInfo
    {
        public string Action = string.Empty;
        public string Area = string.Empty;
        public string Controller = string.Empty;
        public IDictionary<string, string> RequestParams;

        internal RequestInfo()
        {
            RequestParams = new Dictionary<string, string>();
        }

        internal RequestInfo(HttpRequestBase request)
        {
            Area = request.RequestContext.RouteData.DataTokens["area"].SafeToString();
            Controller = request.RequestContext.RouteData.Values["controller"].SafeToString();
            Action = request.RequestContext.RouteData.Values["action"].SafeToString();
            RequestParams = ExtractParamsFromRequest(request);
        }

        private static Dictionary<string, string> ExtractParamsFromRequest(HttpRequestBase request)
        {
            var result = new Dictionary<string, string>();
            foreach (string key in request.QueryString.Keys)
                result.Add(key, request.QueryString[key]);

            foreach (var param in request.RequestContext.RouteData.Values)
            {
                if (param.Key != "controller" && param.Key != "action")
                    result.Add(param.Key, param.Value.SafeToString());
            }

            return result;
        } 
    }
}
