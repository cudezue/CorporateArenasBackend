using CorporateArenasBackend.Models.User;
using CorporateArenasBackend.Utilities;
using System.Linq;

namespace CorporateArenasBackend.Repositories
{
    public abstract class BaseRepository : IBaseRepository
    {
        public virtual bool HasPermission(UserDto user, Entities entity, Actions action)
        {
            var canPerformAction = user.Role.Permissions.Where(p => p.Entity == entity.ToString() && p.Action == action.ToString()).FirstOrDefault();
            return canPerformAction != null;
        }
    }
}