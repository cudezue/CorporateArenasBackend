using System.Collections.Generic;
using System.Threading.Tasks;
using CorporateArenasBackend.Models.BrainTeaser;

namespace CorporateArenasBackend.Repositories.BrainTeaser
{
    public interface IBrainTeaserRepository
    {
        Task<ICollection<BrainTeaserDto>> Get();

        Task<BrainTeaserDto> GetById(int id);

        Task<BrainTeaserDto> Create(BrainTeaserRequest model);

        Task<BrainTeaserDto> Update(int id, BrainTeaserRequest model);

        Task<BrainTeaserCommentDto> AddComment(int id, BrainTeaserCommentRequest model);

        Task Delete(int id);
    }
}