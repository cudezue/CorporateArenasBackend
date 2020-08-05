using CorporateArenasBackend.Infrastructure;
using CorporateArenasBackend.Models.User;
using CorporateArenasBackend.Repositories.User;
using CorporateArenasBackend.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult<ICollection<UserDto>>> Index()
        {
            var hasPermission = !_repository
                .HasPermission(await _repository.GetById(User.GetId()), Entities.User, Actions.Create);

            if (hasPermission)
                return Unauthorized(new { Message = "You are not authorized to perform this action" });

            return Ok(await _repository.Get());
        }

        [Route(nameof(Create))]
        [HttpPost]
        public async Task<ActionResult<UserDto>> Create(CreateUserRequestModel model)
        {
            var hasPermission = !_repository
                .HasPermission(await _repository.GetById(User.GetId()), Entities.User, Actions.Create);

            if (hasPermission)
                return Unauthorized(new { Message = "You are not authorized to perform this action" });

            var user = await _repository.Create(model);

            return user != null
                ? Created(nameof(Create), user)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }

        [Route(nameof(Update))]
        [HttpPatch]
        public async Task<ActionResult<UserDto>> Update(UpdateUserRequestModel model)
        {
            var user = await _repository.Update(User.GetId(), model);

            return user != null
                ? Accepted(nameof(Update), model)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }
    }
}