using CorporateArenasBackend.Data;
using CorporateArenasBackend.Models.Permission;
using CorporateArenasBackend.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Repositories.User
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<Data.Models.User> _userManager;
        private readonly ApplicationSettings _appSettings;

        public UserRepository(ApplicationDbContext db, UserManager<Data.Models.User> userManager, IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _db = db;
        }

        public async Task<ICollection<UserDto>> Get()
            => await _db.Users
                .Include(u => u.Role)
                .ThenInclude(r => r.Permissions)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    OtherName = u.OtherName,
                    PhoneNumber = u.PhoneNumber,
                    Email = u.Email,
                    Username = u.UserName,
                    Role = new Models.Role.RoleDto
                    {
                        Id = u.Role.Id,
                        Name = u.Role.Name,
                        Description = u.Role.Description,
                        Permissions = u.Role.Permissions
                        .Select(p => new PermissionDto
                        {
                            Name = p.Permission.Name,
                            Action = p.Permission.Action,
                            Description = p.Permission.Description,
                            Entity = p.Permission.Entity
                        }).ToList()
                    }
                })
                .ToListAsync();

        public async Task<UserDto> GetById(string id)
            => await _db.Users
                .Include(u => u.Role)
                .ThenInclude(r => r.Permissions)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    OtherName = u.OtherName,
                    PhoneNumber = u.PhoneNumber,
                    Email = u.Email,
                    Username = u.UserName,
                    Role = new Models.Role.RoleDto
                    {
                        Id = u.Role.Id,
                        Name = u.Role.Name,
                        Description = u.Role.Description,
                        Permissions = u.Role.Permissions
                        .Select(p => new PermissionDto
                        {
                            Name = p.Permission.Name,
                            Action = p.Permission.Action,
                            Description = p.Permission.Description,
                            Entity = p.Permission.Entity
                        }).ToList()
                    }
                })
                .FirstOrDefaultAsync(u => u.Id == id);

        public async Task<UserDto> FindByUserName(string userName)
            => await _db.Users
                .Include(u => u.Role)
                .ThenInclude(r => r.Permissions)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    OtherName = u.OtherName,
                    PhoneNumber = u.PhoneNumber,
                    Email = u.Email,
                    Username = u.UserName,
                    Role = new Models.Role.RoleDto
                    {
                        Id = u.Role.Id,
                        Name = u.Role.Name,
                        Description = u.Role.Description,
                        Permissions = u.Role.Permissions
                        .Select(p => new PermissionDto
                        {
                            Name = p.Permission.Name,
                            Action = p.Permission.Action,
                            Description = p.Permission.Description,
                            Entity = p.Permission.Entity
                        }).ToList()
                    }
                })
                .FirstOrDefaultAsync(u => u.Username == userName);

        public async Task<UserDto> Create(CreateUserRequestModel model)
        {
            var user = new Data.Models.User
            {
                UserName = model.UserName,
                Email = model.UserName,
                RoleId = model.Role
            };

            await _userManager.CreateAsync(user, model.Password);

            return await FindByUserName(user.UserName);
        }

        public async Task<UserDto> Update(string id, UpdateUserRequestModel model)
        {
            var user = await _userManager.FindByIdAsync(id);

            user.FirstName = model.FirstName;
            user.OtherName = model.OtherName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;

            _db.Users.Update(user);
            await _db.SaveChangesAsync();

            return await GetById(user.Id);
        }

        public async Task Delete(Data.Models.User user)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }

        public string GenerateJwtToken(string userId, string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}