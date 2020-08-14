using CorporateArenasBackend.Models.Vacancy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Repositories.Vacancy
{
    public interface IJobTypeRepository
    {
        Task<ICollection<JobTypeDto>> Get();

        Task<JobTypeDto> GetById(int id);

        Task<JobTypeDto> Create(JobTypeRequest model);

        Task<JobTypeDto> Update(int id, JobTypeRequest model);

        Task Delete(int id);
    }
}