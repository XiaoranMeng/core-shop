using System.Linq;
using System.Security.Claims;

namespace Web.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetEmailFromPrincipal(this ClaimsPrincipal user)
        {
            return user?.Claims?
                .FirstOrDefault(c => c.Type == ClaimTypes.Email)?
                .Value;
        }
    }
}
