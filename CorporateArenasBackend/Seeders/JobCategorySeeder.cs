using System.Collections.Generic;
using System.Linq;
using CorporateArenasBackend.Data;
using CorporateArenasBackend.Data.Models;

namespace CorporateArenasBackend.Seeders
{
    public static class JobCategorySeeder
    {
        public static void Run(ApplicationDbContext context)
        {
            if (context.JobCategories.Any()) return;

            var jobCategories = new List<JobCategory>
            {
                new JobCategory
                {
                    //Id = 1,
                    Description =
                        "Information and Communications Technology (ICT) refers to all the technology used to handle telecommunications, broadcast media, intelligent building management systems, audiovisual processing and transmission systems, and network-based control and monitoring functions",
                    Name = "Information and Communications Technology"
                },
                
                new JobCategory
                {
                    //Id = 2,
                    Description = "Project management is the process of leading the work of a team to achieve goals and meet success criteria at a specified time",
                    Name = "Project Management"
                }
            };
            
            context.JobCategories.AddRange(jobCategories);
            context.SaveChanges();
        }
    }
}