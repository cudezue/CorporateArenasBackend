using System.Collections.Generic;
using System.Threading.Tasks;
using CorporateArenasBackend.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CorporateArenasBackend.Repositories.User
{
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<Data.Models.User> _userManager;

        public UserRepository(ApplicationDbContext db, UserManager<Data.Models.User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<ICollection<Data.Models.User>> Get()
        {
            var users = await _db.Users.ToListAsync();

            return users;
        }

        public async Task<Data.Models.User> GetById(string id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<Data.Models.User> Create(string email, string username, string password)
        {
            var user = new Data.Models.User
            {
                UserName = username,
                Email = email
            };
            
            await _userManager.CreateAsync(user, password);

            return user;
        }

        public async Task<Data.Models.User> Update(string id, string firstName, string lastName, string otherName)
        {
            var user = new Data.Models.User
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                OtherName = otherName,
            };

            _db.Users.Update(user);
            await _db.SaveChangesAsync();

            return user;
        }

        public async Task Delete(Data.Models.User user)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }
    }
}