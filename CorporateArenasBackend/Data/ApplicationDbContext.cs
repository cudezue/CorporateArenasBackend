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

        public DbSet<TrafficUpdate> TrafficUpdates { get; set; }

        public DbSet<BrainTeaser> BrainTeasers { get; set; }

        public DbSet<BrainTeaserComment> BrainTeaserComments { get; set; }

        public DbSet<TrafficUpdateComment> TrafficUpdateComments { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<ArticleComment> ArticleComments { get; set; }

        public DbSet<Vacancy> Vacancies { get; set; }

        public DbSet<JobCategory> JobCategories { get; set; }

        public DbSet<JobType> JobTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RolePermission>()
                .HasKey(rolePermission => new {rolePermission.RoleId, rolePermission.PermissionId});

            builder.Entity<TrafficUpdate>()
                .Property(trafficUpdate => trafficUpdate.CreatedAt)
                .HasDefaultValueSql("getutcdate()");

            builder.Entity<Vacancy>()
                .Property(vacancy => vacancy.CreatedAt)
                .HasDefaultValueSql("getutcdate()");
            
            builder.Entity<BrainTeaser>()
                .Property(brainTeaser => brainTeaser.CreatedAt)
                .HasDefaultValueSql("getutcdate()");

            builder.Entity<BrainTeaserComment>()
                .Property(brainTeaserComment => brainTeaserComment.CreatedAt)
                .HasDefaultValueSql("getutcdate()");

            builder.Entity<TrafficUpdateComment>()
                .Property(trafficUpdateComment => trafficUpdateComment.CreatedAt)
                .HasDefaultValueSql("getutcdate()");

            builder.Entity<ArticleComment>()
                .Property(articleComment => articleComment.CreatedAt)
                .HasDefaultValueSql("getutcdate()");

            builder.Entity<Article>()
                .Property(article => article.CreatedAt)
                .HasDefaultValueSql("getutcdate()");

            builder.Entity<Role>()
                .HasMany(role => role.Users)
                .WithOne(user => user.Role)
                .HasForeignKey(user => user.RoleId);

            builder.Entity<RolePermission>()
                .HasOne(rolePermissions => rolePermissions.Role)
                .WithMany(role => role.Permissions)
                .HasForeignKey(rolePermission => rolePermission.RoleId);

            builder.Entity<RolePermission>()
                .HasOne(rolePermissions => rolePermissions.Permission)
                .WithMany(permission => permission.Roles)
                .HasForeignKey(rolePermission => rolePermission.PermissionId);

            builder.Entity<BrainTeaser>()
                .HasMany(brainTeaser => brainTeaser.Comments)
                .WithOne(comment => comment.BrainTeaser)
                .HasForeignKey(comment => comment.BrainTeaserId);

            builder.Entity<TrafficUpdate>()
                .HasMany(trafficUpdate => trafficUpdate.Comments)
                .WithOne(comment => comment.TrafficUpdate)
                .HasForeignKey(comment => comment.TrafficUpdateId);

            builder.Entity<TrafficUpdate>()
                .HasIndex(trafficUpdate => trafficUpdate.Title)
                .IsUnique();

            builder.Entity<Article>()
                .HasMany(article => article.Comments)
                .WithOne(comment => comment.Article)
                .HasForeignKey(comment => comment.ArticleId);
            
            builder.Entity<JobCategory>()
                .HasMany(jobCategory => jobCategory.Vacancies)
                .WithOne(vacancy => vacancy.JobCategory)
                .HasForeignKey(vacancy => vacancy.JobCategoryId);
            
            builder.Entity<JobType>()
                .HasMany(jobType => jobType.Vacancies)
                .WithOne(vacancy => vacancy.JobType)
                .HasForeignKey(vacancy => vacancy.JobTypeId);

            builder.Entity<Article>()
                .HasIndex(article => article.Title)
                .IsUnique();
            

            builder.SeedRoleTable();
            builder.SeedPermissionTable();

            base.OnModelCreating(builder);
        }
    }
}