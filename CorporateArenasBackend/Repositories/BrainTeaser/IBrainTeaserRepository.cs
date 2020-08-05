using CorporateArenasBackend.Models.BrainTeaser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Repositories.BrainTeaser
{
    interface IBrainTeaserRepository
    {
        Task<ICollection<BrainTeaserDto>> Get();

        Task<BrainTeaserDto> GetById(int id);

        Task<BrainTeaserDto> Create(BrainTeaserRequest model);

        Task<BrainTeaserDto> Update(int id, BrainTeaserRequest model);

        Task Delete(int id);
    }
}
