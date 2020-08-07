using System.Collections.Generic;
using System.Threading.Tasks;
using CorporateArenasBackend.Models.Vacancy;

namespace CorporateArenasBackend.Repositories.Vacancy
{
    public interface IJobCategoryRepository
    {
        Task<ICollection<JobCategoryDto>> Get();

        Task<JobCategoryDto> GetById(int id);

        Task<JobCategoryDto> Create(JobCategoryRequest model);
        
        Task<JobCategoryDto> Update(int id, JobCategoryRequest model);

        Task Delete(int id);
    }
}