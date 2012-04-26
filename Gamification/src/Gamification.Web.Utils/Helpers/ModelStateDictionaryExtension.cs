using System.Web.Mvc;

namespace Gamification.Web.Utils.Helpers
{
    public static class ModelStateDictionaryExtension
    {
        public static void AddGeneralError(this ModelStateDictionary modelState, string error)
        {
            modelState.AddModelError(string.Empty, error);
        }
    }
}
