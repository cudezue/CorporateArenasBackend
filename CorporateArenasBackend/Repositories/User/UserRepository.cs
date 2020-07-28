using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CorporateArenasBackend.Data;
using CorporateArenasBackend.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CorporateArenasBackend.Repositories.User
{
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<Data.Models.User> _userManager;
        private readonly ApplicationSettings _appSettings;

        public UserRepository(ApplicationDbContext db, UserManager<Data.Models.User> userManager, IOptions<ApplicationSettings> appSettings)
        {
            _db = db;
            _userManager = userManager;
            _appSettings = appSettings.Value;
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

        public async Task<Data.Models.User> FindByUserName(string userName)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            return user;
        }

        public async Task<Data.Models.User> Create(CreateUserRequestModel model)
        {
            var user = new Data.Models.User
            {
                UserName = model.UserName,
                Email = model.UserName,
            };
            
            await _userManager.CreateAsync(user, model.Password);

            return user;
        }

        public async Task<Data.Models.User> Update(string id, UpdateUserRequestModel model)
        {
            var user = new Data.Models.User
            {
                Id = id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                OtherName = model.OtherName,
                PhoneNumber = model.PhoneNumber
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

        public string GenerateJwtToken(Data.Models.User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}