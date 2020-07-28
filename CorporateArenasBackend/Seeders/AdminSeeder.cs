using System.Linq;
using CorporateArenasBackend.Data;
using CorporateArenasBackend.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace CorporateArenasBackend.Seeders
{
    public static class AdminSeeder
    {
        public static void Run(ApplicationDbContext context)
        {
            if (context.Users.Any()) return;
            
            var hasher = new PasswordHasher<User>();
            var admin = new User
            {
                FirstName = "Chukwunwike",
                LastName = "Udezue",
                UserName = "wyke42",
                Email = "cudezue@inspirecoders.com",
                PasswordHash = hasher.HashPassword(null, "secret123"),
                PhoneNumber = "+2348038614110"
            };

            context.Users.Add(admin);
            context.SaveChanges();
        }
    }
}