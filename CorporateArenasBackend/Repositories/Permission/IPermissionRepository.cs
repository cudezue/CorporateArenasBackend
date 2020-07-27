using System.Collections.Generic;
using System.Threading.Tasks;
using CorporateArenasBackend.Models.Permission;

namespace CorporateArenasBackend.Repositories.Permission
{
    public interface IPermissionRepository
    {
        Task<ICollection<Data.Models.Permission>> Get();
        Task<Data.Models.Permission> GetById(string id);
        Task<Data.Models.Permission> Create(PermissionRequestModel model);
        Task<Data.Models.Permission> Update(string id, PermissionRequestModel model);
        Task Delete(Data.Models.Permission permission);
    }
}