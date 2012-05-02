using System.Security.Principal;
using System.Web.Security;

namespace NerdDinner.Helpers
{
    public static class PrincipalHelpers
    {
        public static NerdIdentity NerdUser(this IPrincipal principal)
        {
            if (principal.Identity.IsAuthenticated)
            {
                return (NerdIdentity)principal.Identity;
            }

            return new NerdIdentity(new FormsAuthenticationTicket("", false, 0));
        }
    }
}