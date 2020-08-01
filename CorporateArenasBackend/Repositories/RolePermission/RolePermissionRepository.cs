using CorporateArenasBackend.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Repositories.RolePermission
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly ApplicationDbContext _db;

        public RolePermissionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<Data.Models.RolePermission>> Get()
        {
            var roles = await _db.RolePermissions.ToListAsync();

            return roles;
        }

        public async Task<Data.Models.RolePermission> Create(int roleId, int permissionId)
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