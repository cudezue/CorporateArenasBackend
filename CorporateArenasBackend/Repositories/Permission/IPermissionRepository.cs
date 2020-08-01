using CorporateArenasBackend.Models.Permission;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Repositories.Permission
{
    public interface IPermissionRepository : IBaseRepository
    {
        Task<ICollection<PermissionDto>> Get();

        Task<PermissionDto> GetById(int id);

        Task<PermissionDto> Create(PermissionRequestModel model);

        Task<PermissionDto> Update(int id, PermissionRequestModel model);

        Task Delete(int id);
    }
}