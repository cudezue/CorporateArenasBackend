using System.Threading.Tasks;

namespace CorporateArenasBackend.Repositories.UserRole
{
    public interface IUserRoleRepository
    {
        Task<Data.Models.UserRole> Create(string userId, string roleId);
        Task<Data.Models.UserRole> Update(int id, string roleId, string userId);
        Task Delete(Data.Models.UserRole userRole);
    }
}