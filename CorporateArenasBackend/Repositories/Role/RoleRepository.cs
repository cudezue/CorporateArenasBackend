using System.Collections.Generic;
using System.Threading.Tasks;
using CorporateArenasBackend.Data;
using CorporateArenasBackend.Models.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CorporateArenasBackend.Repositories.Role
{
    public class RoleRepository: IRoleRepository
    {
        private readonly ApplicationDbContext _db;

        public RoleRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<IdentityRole>> Get()
        {
            var roles = await _db.Roles.ToListAsync();

            return roles;
        }

        public async Task<IdentityRole> GetById(string id)
        {
            return await _db.Roles.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IdentityRole> Create(RoleRequestModel model)
        {
            var role = new Data.Models.Role
            {
                Name = model.Name,
                Description = model.Description
            };

            _db.Roles.Add(role);
            await _db.SaveChangesAsync();

            return role;
        }

        public async Task<IdentityRole> Update(string id, RoleRequestModel model)
        {
            var role = new Data.Models.Role
            {
                Id = id,
                Name = model.Name,
                Description = model.Description
            };

            _db.Roles.Update(role);
            await _db.SaveChangesAsync();

            return role;
        }

        public async Task Delete(IdentityRole role)
        {
            _db.Roles.Remove(role);
            await _db.SaveChangesAsync();
        }
    }
}