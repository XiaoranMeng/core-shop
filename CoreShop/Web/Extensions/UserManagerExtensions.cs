using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<ApplicationUser> FindUserWithAddressAsync(this UserManager<ApplicationUser> userManager, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            return await userManager.Users.Include(u => u.Address).SingleOrDefaultAsync(u => u.Email == email);
        }

        public static async Task<ApplicationUser> FindUserAsync(this UserManager<ApplicationUser> userManager, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            return await userManager.Users.SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}
