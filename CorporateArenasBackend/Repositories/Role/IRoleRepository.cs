using System.Collections.Generic;
using System.Threading.Tasks;
using CorporateArenasBackend.Models.Role;
using Microsoft.AspNetCore.Identity;

namespace CorporateArenasBackend.Repositories.Role
{
    public interface IRoleRepository
    {
        Task<ICollection<IdentityRole>> Get();
        Task<IdentityRole> GetById(string id);
        Task<IdentityRole> Create(RoleRequestModel model);
        Task<IdentityRole> Update(string id, RoleRequestModel model);
        Task Delete(IdentityRole role);
    }
}