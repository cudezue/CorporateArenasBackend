using CorporateArenasBackend.Data.Models;
using CorporateArenasBackend.Models.User;
using CorporateArenasBackend.Repositories.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Controllers
{
    public class LoginController : ApiController
    {
        private static readonly object LoginErrorMessage = new { Message = "Invalid Username/Password" };
        private readonly IUserRepository _repository;
        private readonly UserManager<User> _userManager;

        public LoginController(UserManager<User> userManager, IUserRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
        }

        [Route("")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseModel>> Index(LoginRequestModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return Unauthorized(LoginErrorMessage);

            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!passwordValid)
                return Unauthorized(LoginErrorMessage);

            return Ok(new LoginResponseModel
            {
                Token = _repository.GenerateJwtToken(user.Id, user.UserName),
                User = await _repository.FindByUserName(user.UserName)
            });
        }
    }
}