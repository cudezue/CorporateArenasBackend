using System.Linq;
using CorporateArenasBackend.Data;
using CorporateArenasBackend.Data.Models;

namespace CorporateArenasBackend.Seeders
{
    public static class UserRoleSeeder
    {
        public static void Run(ApplicationDbContext context)
        {
            if (context.UserRoles.Any()) return;
            var user = context.Users.FirstOrDefault();
            var role = context.Roles.FirstOrDefault();

            var userRole = new UserRole
            {
                RoleId = role!.Id,
                UserId = user!.Id
            };

            context.UserRoles.Add(userRole);
            context.SaveChanges();
        }
    }
}