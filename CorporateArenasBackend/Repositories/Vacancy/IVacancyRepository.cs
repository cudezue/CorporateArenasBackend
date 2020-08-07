using System.Collections.Generic;
using System.Threading.Tasks;
using CorporateArenasBackend.Models.Vacancy;

namespace CorporateArenasBackend.Repositories.Vacancy
{
    public interface IVacancyRepository
    {
        Task<ICollection<VacancyDto>> Get();

        Task<VacancyDto> GetById(int id);

        Task<VacancyDto> Create(VacancyRequest model);
        
        Task<VacancyDto> Update(int id, VacancyRequest model);

        Task Delete(int id);
    }
}