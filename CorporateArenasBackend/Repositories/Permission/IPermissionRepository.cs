using System.Collections.Generic;
using System.Threading.Tasks;
using CorporateArenasBackend.Models.Permission;

namespace CorporateArenasBackend.Repositories.Permission
{
    public interface IPermissionRepository
    {
        Task<ICollection<Data.Models.Permission>> Get();
        Task<Data.Models.Permission> GetById(int id);
        Task<Data.Models.Permission> Create(PermissionRequestModel model);
        Task<Data.Models.Permission> Update(int id, PermissionRequestModel model);
        Task Delete(Data.Models.Permission permission);
    }
}