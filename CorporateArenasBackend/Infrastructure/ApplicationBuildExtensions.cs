using CorporateArenasBackend.Data;
using CorporateArenasBackend.Data.Models;
using CorporateArenasBackend.Seeders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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
            var userManager = services.ServiceProvider.GetService<UserManager<User>>();

            dbContext.Database.Migrate();
            RolePermissionSeeder.Run(dbContext);
            AdminSeeder.RunAsync(userManager).GetAwaiter().GetResult();
            JobTypeSeeder.Run(dbContext);
            JobCategorySeeder.Run(dbContext);
        }
    }
}