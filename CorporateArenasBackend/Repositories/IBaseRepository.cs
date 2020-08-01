using CorporateArenasBackend.Models.User;
using CorporateArenasBackend.Utilities;

namespace CorporateArenasBackend.Repositories
{
    public interface IBaseRepository
    {
        bool HasPermission(UserDto userId, Entities entity, Actions action);
    }
}