using CorporateArenasBackend.Data.Models;
using CorporateArenasBackend.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Controllers
{
    public class UserController : ApiController
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
            => _userManager = userManager;

        [Route(nameof(Create))]
        [HttpPost]
        public async Task<ActionResult> Create(CreateUserRequestModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors);
        }

        [Route(nameof(Test))]
        public ActionResult Test()
        {
            return Ok(new { Message = "Test Message" });
        }
    }
}