using CorporateArenasBackend.Data;
using CorporateArenasBackend.Seeders;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CorporateArenasBackend.Infrastructure
{
    public static class ApplicationBuildExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<ApplicationDbContext>();

            dbContext.Database.Migrate();
            RolePermissionSeeder.Run(dbContext);
            AdminSeeder.Run(dbContext);
            UserRoleSeeder.Run(dbContext);
        }
    }
}