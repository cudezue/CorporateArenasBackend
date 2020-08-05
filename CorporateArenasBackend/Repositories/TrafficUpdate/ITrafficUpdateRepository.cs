using CorporateArenasBackend.Models.TrafficUpdate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Repositories.TrafficUpdate
{
    public interface ITrafficUpdateRepository : IBaseRepository
    {
        Task<ICollection<TrafficUpdateDto>> GetPublished();

        Task<ICollection<TrafficUpdateDto>> Get();

        Task<TrafficUpdateDto> GetById(int id);

        Task<TrafficUpdateDto> GetBySlug(string slug);

        Task<TrafficUpdateDto> Create(TrafficUpdateRequest model);

        Task<TrafficUpdateDto> Update(int id, TrafficUpdateRequest model);

        Task Delete(int id);
    }
}