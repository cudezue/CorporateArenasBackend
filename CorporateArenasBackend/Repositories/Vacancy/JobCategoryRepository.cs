using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorporateArenasBackend.Data;
using CorporateArenasBackend.Data.Models;
using CorporateArenasBackend.Models.Vacancy;
using Microsoft.EntityFrameworkCore;

namespace CorporateArenasBackend.Repositories.Vacancy
{
    public class JobCategoryRepository : BaseRepository, IJobCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public JobCategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<JobCategoryDto>> Get()
        {
            return await _db.JobCategories
                .Include(jobCategory => jobCategory.Vacancies)
                .Select(jobCategory => new JobCategoryDto
                {
                    Id = jobCategory.Id,
                    Description = jobCategory.Description,
                    Name = jobCategory.Name,
                    Vacancies = jobCategory.Vacancies
                        .OrderByDescending(vacancy => vacancy.CreatedAt)
                        .Select(vacancy => new VacancyDto
                    {
                        Description = vacancy.Description,
                        Employee = vacancy.Employee,
                        Id = vacancy.Id,
                        Location = vacancy.Location,
                        Title = vacancy.Title,
                        CreatedAt = vacancy.CreatedAt,
                        JobType = new JobTypeDto {Name = vacancy.JobType.Name}
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<JobCategoryDto> GetById(int id)
        {
            return await _db.JobCategories
                .Include(jobCategory => jobCategory.Vacancies)
                .OrderByDescending(jobCategory => jobCategory.Name)
                .Select(jobCategory => new JobCategoryDto
                {
                    Id = jobCategory.Id,
                    Description = jobCategory.Description,
                    Name = jobCategory.Name,
                    Vacancies = jobCategory.Vacancies
                        .OrderByDescending(vacancy => vacancy.CreatedAt)
                        .Select(vacancy => new VacancyDto
                    {
                        Description = vacancy.Description,
                        Employee = vacancy.Employee,
                        Id = vacancy.Id,
                        Location = vacancy.Location,
                        Title = vacancy.Title,
                        CreatedAt = vacancy.CreatedAt,
                        JobType = new JobTypeDto {Name = vacancy.JobType.Name}
                    }).ToList()
                }).FirstOrDefaultAsync(jobCategory => jobCategory.Id == id);
        }

        public async Task<JobCategoryDto> Create(JobCategoryRequest model)
        {
            var jobCategory = new JobCategory
            {
                Description = model.Description,
                Name = model.Name
            };

            _db.JobCategories.Add(jobCategory);
            await _db.SaveChangesAsync();

            return await GetById(jobCategory.Id);
        }

        public async Task<JobCategoryDto> Update(int id, JobCategoryRequest model)
        {
            var jobCategory = await _db.JobCategories.FindAsync(id);

            jobCategory.Description = model.Description;
            jobCategory.Name = model.Name;

            _db.JobCategories.Update(jobCategory);

            await _db.SaveChangesAsync();

            return await GetById(jobCategory.Id);
        }

        public async Task Delete(int id)
        {
            var jobCategory = await _db.JobCategories.FindAsync(id);
            _db.JobCategories.Remove(jobCategory);
            await _db.SaveChangesAsync();
        }
    }
}