using System;
using System.Collections.Generic;
using CorporateArenasBackend.Data;
using CorporateArenasBackend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CorporateArenasBackend.Infrastructure
{
    public static class ModelBuilderExtensions
    {
        public static void SeedRoleTable(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    Description =
                        "Administrator determines the site policies, appoints moderators and manages the technical operations"
                }
            );
        }

        public static void SeedPermissionTable(this ModelBuilder modelBuilder)
        {
            var entities = new[] {"User", "Role", "Permission"};
            var permissions = new List<Permission>();

            foreach (var entity in entities)
            {
                permissions.Add(
                    new Permission
                    {
                        Name = string.Format("Create {0}", entity.ToLower()),
                        Action = "Create",
                        Description = string.Format("Creates a {0} resource", entity),
                        Entity = entity
                    }
                );
                permissions.Add(
                    new Permission
                    {
                        Name = string.Format("Read {0}", entity.ToLower()),
                        Action = "Read",
                        Description = string.Format("Reads a {0} resource", entity),
                        Entity = entity
                    }
                );
                permissions.Add(
                    new Permission
                    {
                        Name = string.Format("Update {0}", entity.ToLower()),
                        Action = "Update",
                        Description = string.Format("Updates a {0} resource", entity),
                        Entity = entity
                    }
                );
                permissions.Add(
                    new Permission
                    {
                        Name = string.Format("Delete {0}", entity.ToLower()),
                        Action = "Delete",
                        Description = string.Format("Remove a {0} resource", entity),
                        Entity = entity
                    }
                );
            }

            var permissionListToBeSeeded = new List<Permission>();

            for (var i = 0; i < permissions.Count; i++)
            {
                permissions[i].Id = i + 1;
                permissionListToBeSeeded.Add(permissions[i]);
            }

            modelBuilder.Entity<Permission>().HasData(permissionListToBeSeeded);
        }

        public static async void SeedRolePermissionTable(this ModelBuilder modelBuilder, ApplicationDbContext context)
        {
            var permissions = await context.Permissions.ToListAsync();
            var role = await context.Roles.FirstOrDefaultAsync();
            var rolePermissions = new List<RolePermission>();

            foreach (var permission in permissions)
                rolePermissions.Add(
                    new RolePermission
                    {
                        RoleId = role.Id,
                        PermissionId = permission.Id
                    }
                );

            var rolePermissionListToBeSeeded = new List<RolePermission>();

            for (var i = 0; i < rolePermissions.Count; i++)
            {
                rolePermissions[i].Id = i + 1;
                rolePermissionListToBeSeeded.Add(rolePermissions[i]);
            }

            modelBuilder.Entity<RolePermission>().HasData(rolePermissionListToBeSeeded);
        }
    }
}