using System.Collections.Generic;
using System.Threading.Tasks;
using CorporateArenasBackend.Models.User;

namespace CorporateArenasBackend.Repositories.User
{
    public interface IUserRepository
    {
        Task<ICollection<Data.Models.User>> Get();
        Task<Data.Models.User> GetById(string id);
        Task<Data.Models.User> FindByUserName(string userName);
        Task<Data.Models.User> Create(CreateUserRequestModel model);
        Task<Data.Models.User> Update(string id, UpdateUserRequestModel model);
        Task Delete(Data.Models.User user);
        string GenerateJwtToken(Data.Models.User user);
    }
}