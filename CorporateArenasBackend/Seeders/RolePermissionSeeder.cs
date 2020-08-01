using CorporateArenasBackend.Data;
using System.Linq;

namespace CorporateArenasBackend.Seeders
{
    public static class RolePermissionSeeder
    {
        public static void Run(ApplicationDbContext context)
        {
            if (context.RolePermissions.Any()) return;
            var role = context.Roles.FirstOrDefault();
            var permissions = context.Permissions.ToList();
            var rolePermissions = permissions.Select(permission => new Data.Models.RolePermission { RoleId = role.Id, PermissionId = permission.Id }).ToList();

            context.RolePermissions.AddRange(rolePermissions);
            context.SaveChanges();
        }
    }
}