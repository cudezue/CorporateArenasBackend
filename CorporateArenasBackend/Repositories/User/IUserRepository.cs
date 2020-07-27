using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Repositories.User
{
    public interface IUserRepository
    {
        Task<ICollection<Data.Models.User>> Get();
        Task<Data.Models.User> GetById(string id);
        Task<Data.Models.User> Create(string email, string username, string password);
        Task<Data.Models.User> Update(string id, string firstName, string lastName, string otherName);
        Task Delete(Data.Models.User user);
    }
}