using CorporateArenasBackend.Models.Role;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Repositories.Role
{
    public interface IRoleRepository : IBaseRepository
    {
        Task<ICollection<RoleDto>> Get();

        Task<RoleDto> GetById(int id);

        Task<RoleDto> Create(RoleRequestModel model);

        Task<RoleDto> Update(int id, RoleRequestModel model);

        Task Delete(int id);
    }
}