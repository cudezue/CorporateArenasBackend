using CorporateArenasBackend.Data;
using CorporateArenasBackend.Models.Permission;
using CorporateArenasBackend.Models.Role;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Repositories.Role
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        private readonly ApplicationDbContext _db;

        public RoleRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<RoleDto>> Get() => await _db.Roles
                .Include(role => role.Users)
                .Include(role => role.Permissions)
                .ThenInclude(rp => rp.Permission)
                .Select(role => new RoleDto
                {
                    Name = role.Name,
                    Description = role.Description,
                    Id = role.Id,
                    Permissions = role.Permissions.Select(p => new PermissionDto
                    {
                        Name = p.Permission.Name,
                        Description = p.Permission.Description,
                        Entity = p.Permission.Entity,
                        Action = p.Permission.Action
                    }).ToList()
                })
                .ToListAsync();

        public async Task<RoleDto> GetById(int id)
            => await _db.Roles
                .Include(role => role.Users)
                .Include(role => role.Permissions)
                .ThenInclude(rp => rp.Permission)
                .Select(role => new RoleDto
                {
                    Name = role.Name,
                    Description = role.Description,
                    Id = role.Id,
                    Permissions = role.Permissions.Select(p => new PermissionDto
                    {
                        Name = p.Permission.Name,
                        Description = p.Permission.Description,
                        Entity = p.Permission.Entity,
                        Action = p.Permission.Action
                    }).ToList()
                })
                .FirstOrDefaultAsync(r => r.Id == id);

        public async Task<RoleDto> Create(RoleRequestModel model)
        {
            var role = new Data.Models.Role
            {
                Name = model.Name,
                Description = model.Description
            };

            _db.Roles.Add(role);
            await _db.SaveChangesAsync();

            return await GetById(role.Id);
        }

        public async Task<RoleDto> Update(int id, RoleRequestModel model)
        {
            var role = await _db.Roles.FindAsync(id);

            model.Name = model.Name;
            model.Description = model.Description;

            _db.Roles.Update(role);
            await _db.SaveChangesAsync();

            return await GetById(role.Id);
        }

        public async Task Delete(int id)
        {
            var role = await _db.Roles.FindAsync(id);
            _db.Roles.Remove(role);
            await _db.SaveChangesAsync();
        }
    }
}