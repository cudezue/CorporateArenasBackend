using System.Text;
using CorporateArenasBackend.Data;
using CorporateArenasBackend.Data.Models;
using CorporateArenasBackend.Repositories.Article;
using CorporateArenasBackend.Repositories.BrainTeaser;
using CorporateArenasBackend.Repositories.Permission;
using CorporateArenasBackend.Repositories.Role;
using CorporateArenasBackend.Repositories.RolePermission;
using CorporateArenasBackend.Repositories.TrafficUpdate;
using CorporateArenasBackend.Repositories.User;
using CorporateArenasBackend.Repositories.Vacancy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CorporateArenasBackend.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
            ApplicationSettings appSettings)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }

        public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
        {
            services
                .AddTransient<IArticleRepository, ArticleRepository>()
                .AddTransient<IBrainTeaserRepository, BrainTeaserRepository>()
                .AddTransient<IPermissionRepository, PermissionRepository>()
                .AddTransient<IRoleRepository, RoleRepository>()
                .AddTransient<IRolePermissionRepository, RolePermissionRepository>()
                .AddTransient<ITrafficUpdateRepository, TrafficUpdateRepository>()
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IVacancyRepository, VacancyRepository>()
                .AddTransient<IJobCategoryRepository, JobCategoryRepository>()
                .AddTransient<IJobTypeRepository, JobTypeRepository>();

            return services;
        }
    }
}