using CorporateArenasBackend.Data;
using CorporateArenasBackend.Models.Permission;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Repositories.Permission
{
    public class PermissionRepository : BaseRepository, IPermissionRepository
    {
        private readonly ApplicationDbContext _db;

        public PermissionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<PermissionDto>> Get()
            => await _db.Permissions
                .Include(permission => permission.Roles)
                .ThenInclude(rolePermission => rolePermission.Role)
                .Select(p => new PermissionDto
                {
                    Action = p.Action,
                    Description = p.Description,
                    Entity = p.Entity,
                    Name = p.Name,
                    Roles = p.Roles
                    .Select(r => new Models.Role.RoleDto
                    {
                        Name = r.Role.Name,
                        Description = r.Role.Description
                    }).ToList()
                })
                .ToListAsync();

        public async Task<PermissionDto> GetById(int id)
            => await _db.Permissions
                .Include(permission => permission.Roles)
                .ThenInclude(rolePermission => rolePermission.Role)
                .Select(p => new PermissionDto
                {
                    Action = p.Action,
                    Description = p.Description,
                    Entity = p.Entity,
                    Name = p.Name,
                    Roles = p.Roles
                    .Select(r => new Models.Role.RoleDto
                    {
                        Name = r.Role.Name,
                        Description = r.Role.Description
                    }).ToList()
                })
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<PermissionDto> Create(PermissionRequestModel model)
        {
            var permission = new Data.Models.Permission
            {
                Name = model.Name,
                Description = model.Description,
                Action = model.Action,
                Entity = model.Entity
            };

            _db.Permissions.Add(permission);
            await _db.SaveChangesAsync();

            return await GetById(permission.Id);
        }

        public async Task<PermissionDto> Update(int id, PermissionRequestModel model)
        {
            var permission = await _db.Permissions.FindAsync(id);

            permission.Name = model.Name;
            permission.Description = model.Description;
            permission.Action = model.Action;
            permission.Entity = model.Entity;

            _db.Permissions.Update(permission);
            await _db.SaveChangesAsync();

            return await GetById(permission.Id);
        }

        public async Task Delete(int id)
        {
            var permission = await _db.Permissions.FindAsync(id);

            _db.Permissions.Remove(permission);

            await _db.SaveChangesAsync();
        }
    }
}