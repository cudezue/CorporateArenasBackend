using System.Threading.Tasks;

namespace CorporateArenasBackend.Repositories.RolePermission
{
    public interface IRolePermissionRepository
    {
        Task<Data.Models.RolePermission> Create(string roleId, string permissionId);
        Task Delete(Data.Models.RolePermission rolePermission);
    }
}