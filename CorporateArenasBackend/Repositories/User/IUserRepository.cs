using CorporateArenasBackend.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Repositories.User
{
    public interface IUserRepository : IBaseRepository
    {
        Task<ICollection<UserDto>> Get();

        Task<UserDto> GetById(string id);

        Task<UserDto> FindByUserName(string userName);

        Task<UserDto> Create(CreateUserRequestModel model);

        Task<UserDto> Update(string id, UpdateUserRequestModel model);

        Task Delete(Data.Models.User user);

        string GenerateJwtToken(string userId, string userName);
    }
}