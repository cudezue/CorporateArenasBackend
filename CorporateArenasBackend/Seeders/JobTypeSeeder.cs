using System.Collections.Generic;
using System.Linq;
using CorporateArenasBackend.Data;
using CorporateArenasBackend.Data.Models;

namespace CorporateArenasBackend.Seeders
{
    public static class JobTypeSeeder
    {
        public static void Run(ApplicationDbContext context)
        {
            if (context.JobTypes.Any()) return;
            var jobTypes = new List<JobType>
            {
                new JobType {Name = "Full Time Jobs"},
                new JobType {Name = "Part Time Jobs"},
                new JobType {Name = "Remote Jobs"},
                new JobType {Name = "Freelance Gigs"},
            };
            
            context.JobTypes.AddRange(jobTypes);
            context.SaveChanges();
        }
    }
}