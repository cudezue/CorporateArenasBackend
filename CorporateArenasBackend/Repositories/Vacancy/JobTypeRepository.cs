using CorporateArenasBackend.Data;
using CorporateArenasBackend.Models.Vacancy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Repositories.Vacancy
{
    public class JobTypeRepository : BaseRepository, IJobTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public JobTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<JobTypeDto> Create(JobTypeRequest model)
        {
            var jobType = new Data.Models.JobType { Name = model.Name };

            _db.JobTypes.Add(jobType);
            await _db.SaveChangesAsync();

            return await GetById(jobType.Id);
        }

        public async Task Delete(int id)
        {
            var jobType = _db.JobTypes.Find(id);

            _db.JobTypes.Remove(jobType);

            await _db.SaveChangesAsync();
        }

        public async Task<ICollection<JobTypeDto>> Get()
        {
            return await _db.JobTypes
                .Select(jobType => new JobTypeDto
                {
                    Id = jobType.Id,
                    Name = jobType.Name
                }).ToListAsync();
        }

        public async Task<JobTypeDto> GetById(int id)
        {
            return await _db.JobTypes
                .Select(jobType => new JobTypeDto
                {
                    Id = jobType.Id,
                    Name = jobType.Name
                }).FirstOrDefaultAsync(jobType => jobType.Id == id);
        }

        public async Task<JobTypeDto> Update(int id, JobTypeRequest model)
        {
            var jobType = await _db.JobTypes.FindAsync(id);
            jobType.Name = model.Name;

            _db.JobTypes.Update(jobType);

            await _db.SaveChangesAsync();

            return await GetById(jobType.Id);
        }
    }
}
