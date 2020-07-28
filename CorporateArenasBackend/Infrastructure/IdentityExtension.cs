using System.Linq;
using System.Security.Claims;

namespace CorporateArenasBackend.Infrastructure
{
    public static class IdentityExtension
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}