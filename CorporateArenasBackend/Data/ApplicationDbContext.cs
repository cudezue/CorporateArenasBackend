using CorporateArenasBackend.Data.Models;
using CorporateArenasBackend.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CorporateArenasBackend.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Permission> Permissions { get; set; }
        public new DbSet<User> Users { get; set; }

        public new DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public new DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserRole>().HasKey(ur => new {ur.RoleId, ur.UserId});
            builder.Entity<RolePermission>().HasKey(rp => new {rp.RoleId, rp.PermissionId});

            builder.Entity<User>()
                .HasOne(ur => ur.Role)
                .WithOne(u => u.User)
                .HasForeignKey<UserRole>(fk => fk.UserId);

            builder.Entity<Role>()
                .HasMany(u => u.Users)
                .WithOne(r => r.Role)
                .HasForeignKey(f => f.RoleId);

            builder.Entity<Role>()
                .HasMany(p => p.Permissions)
                .WithOne(r => r.Role)
                .HasForeignKey(f => f.RoleId);

            builder.SeedRoleTable();
            builder.SeedPermissionTable();

            base.OnModelCreating(builder);
        }
    }
}