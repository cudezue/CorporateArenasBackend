using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorporateArenasBackend.Data;
using CorporateArenasBackend.Models.Vacancy;
using Microsoft.EntityFrameworkCore;

namespace CorporateArenasBackend.Repositories.Vacancy
{
    public class VacancyRepository : BaseRepository, IVacancyRepository
    {
        private readonly ApplicationDbContext _db;

        public VacancyRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<VacancyDto>> Get()
        {
            return await _db.Vacancies
                .Include(vacancy => vacancy.JobCategory)
                .Include(vacancy => vacancy.JobType)
                .OrderByDescending(vacancy => vacancy.CreatedAt)
                .Select(vacancy => new VacancyDto
                {
                    Description = vacancy.Description,
                    Employee = vacancy.Employee,
                    Id = vacancy.Id,
                    Location = vacancy.Location,
                    Title = vacancy.Title,
                    JobCategory = new JobCategoryDto
                    {
                        Id = vacancy.JobCategory.Id, Description = vacancy.JobCategory.Description,
                        Name = vacancy.JobCategory.Name
                    },
                    JobType = new JobTypeDto {Name = vacancy.JobType.Name}
                }).ToListAsync();
        }

        public async Task<VacancyDto> GetById(int id)
        {
            return await _db.Vacancies
                .Include(vacancy => vacancy.JobCategory)
                .Include(vacancy => vacancy.JobType)
                .Select(vacancy => new VacancyDto
                {
                    Description = vacancy.Description,
                    Employee = vacancy.Employee,
                    Id = vacancy.Id,
                    Location = vacancy.Location,
                    Title = vacancy.Title,
                    JobCategory = new JobCategoryDto
                    {
                        Id = vacancy.JobCategory.Id, Description = vacancy.JobCategory.Description,
                        Name = vacancy.JobCategory.Name
                    },
                    JobType = new JobTypeDto {Name = vacancy.JobType.Name}
                }).FirstOrDefaultAsync(vacancy => vacancy.Id == id);
        }

        public async Task<VacancyDto> Create(VacancyRequest model)
        {
            var vacancy = new Data.Models.Vacancy
            {
                Description = model.Description,
                Employee = model.Employee,
                Location = model.Location,
                Title = model.Title,
                JobCategoryId = model.JobCategoryId,
                JobTypeId = model.JobTypeId
            };

            _db.Vacancies.Add(vacancy);
            await _db.SaveChangesAsync();

            return await GetById(vacancy.Id);
        }

        public async Task<VacancyDto> Update(int id, VacancyRequest model)
        {
            var vacancy = await _db.Vacancies.FindAsync(id);

            vacancy.Description = model.Description;
            vacancy.Employee = model.Employee;
            vacancy.Location = model.Location;
            vacancy.Title = model.Title;
            vacancy.JobCategoryId = model.JobCategoryId;
            vacancy.JobTypeId = model.JobTypeId;

            _db.Vacancies.Update(vacancy);
            await _db.SaveChangesAsync();

            return await GetById(vacancy.Id);
        }

        public async Task Delete(int id)
        {
            var vacancy = await _db.Vacancies.FindAsync(id);
            _db.Vacancies.Remove(vacancy);
            await _db.SaveChangesAsync();
        }
    }
}