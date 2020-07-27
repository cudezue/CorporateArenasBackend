using System.Collections.Generic;
using System.Threading.Tasks;
using CorporateArenasBackend.Data;
using CorporateArenasBackend.Models.Permission;
using Microsoft.EntityFrameworkCore;

namespace CorporateArenasBackend.Repositories.Permission
{
    public class PermissionRepository: IPermissionRepository
    {
        private readonly ApplicationDbContext _db;

        public PermissionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<Data.Models.Permission>> Get()
        {
            var permissions = await _db.Permissions.ToListAsync();

            return permissions;
        }

        public async Task<Data.Models.Permission> GetById(string id)
        {
            var permission = await _db.Permissions.FirstOrDefaultAsync(p => p.Id == id);

            return permission;
        }

        public async Task<Data.Models.Permission> Create(PermissionRequestModel model)
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

            return permission;
        }

        public async Task<Data.Models.Permission> Update(string id, PermissionRequestModel model)
        {
            var permission = new Data.Models.Permission
            {
                Id = id,
                Name = model.Name,
                Description = model.Description,
                Action = model.Action,
                Entity = model.Entity
            };

            _db.Permissions.Update(permission);
            await _db.SaveChangesAsync();

            return permission;
        }

        public async Task Delete(Data.Models.Permission permission)
        {
            _db.Permissions.Remove(permission);
            await _db.SaveChangesAsync();
        }
    }
}