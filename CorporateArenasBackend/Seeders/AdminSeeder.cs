using CorporateArenasBackend.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Seeders
{
    public static class AdminSeeder
    {
        public static async Task RunAsync(UserManager<User> userManager)
        {
            if (userManager.Users.Any()) return;

            var admin = new User
            {
                FirstName = "Chukwunwike",
                LastName = "Udezue",
                UserName = "wyke42",
                Email = "cudezue@inspirecoders.com",
                PhoneNumber = "+2348038614110",
                RoleId = 1
            };

            await userManager.CreateAsync(admin, "secret123");
        }
    }
}