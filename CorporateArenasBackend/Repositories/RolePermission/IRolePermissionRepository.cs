using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Repositories.RolePermission
{
    public interface IRolePermissionRepository
    {
        Task<ICollection<Data.Models.RolePermission>> Get();
        Task<Data.Models.RolePermission> Create(string roleId, int permissionId);
        Task Delete(Data.Models.RolePermission rolePermission);
    }
}