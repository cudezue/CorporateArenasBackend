using System.Threading.Tasks;
using CorporateArenasBackend.Data;

namespace CorporateArenasBackend.Repositories.UserRole
{
    public class UserRoleRepository: IUserRoleRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRoleRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Data.Models.UserRole> Create(string userId, string roleId)
        {
            var userRole = new Data.Models.UserRole
            {
                UserId = userId,
                RoleId = roleId
            };

            _db.UserRoles.Add(userRole);
            await _db.SaveChangesAsync();

            return userRole;
        }

        public async Task<Data.Models.UserRole> Update(string id, string roleId, string userId)
        {
            var userRole = new Data.Models.UserRole
            {
                Id = id,
                UserId = userId,
                RoleId = roleId
            };

            _db.UserRoles.Update(userRole);
            await _db.SaveChangesAsync();

            return userRole;
        }

        public async Task Delete(Data.Models.UserRole userRole)
        {
            _db.UserRoles.Remove(userRole);
            await _db.SaveChangesAsync();
        }
    }
}