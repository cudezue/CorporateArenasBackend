using System.Threading.Tasks;
using CorporateArenasBackend.Data;

namespace CorporateArenasBackend.Repositories.RolePermission
{
    public class RolePermissionRepository: IRolePermissionRepository
    {
        private readonly ApplicationDbContext _db;

        public RolePermissionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Data.Models.RolePermission> Create(string roleId, string permissionId)
        {
            var rolePermission = new Data.Models.RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId
            };

            _db.RolePermissions.Add(rolePermission);
            await _db.SaveChangesAsync();

            return rolePermission;
        }

        public async Task Delete(Data.Models.RolePermission rolePermission)
        {
            _db.RolePermissions.Remove(rolePermission);
            await _db.SaveChangesAsync();
        }
    }
}