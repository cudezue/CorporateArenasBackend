using System.Collections.Generic;
using System.Threading.Tasks;
using CorporateArenasBackend.Models.Role;

namespace CorporateArenasBackend.Repositories.Role
{
    public interface IRoleRepository
    {
        Task<ICollection<Data.Models.Role>> Get();
        Task<Data.Models.Role> GetById(string id);
        Task<Data.Models.Role> Create(RoleRequestModel model);
        Task<Data.Models.Role> Update(string id, RoleRequestModel model);
        Task Delete(Data.Models.Role role);
    }
}