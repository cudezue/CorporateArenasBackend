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
                new JobType {/*Id = 1,*/ Name = "Full Time Jobs"},
                new JobType {/*Id = 2,*/ Name = "Part Time Jobs"},
                new JobType {/*Id = 3,*/ Name = "Remote Jobs"},
                new JobType {/*Id = 4,*/ Name = "Freelance Gigs"},
            };
            
            context.JobTypes.AddRange(jobTypes);
            context.SaveChanges();
        }
    }
}