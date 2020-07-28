using System.Collections.Generic;
using System.Threading.Tasks;
using CorporateArenasBackend.Data.Models;
using CorporateArenasBackend.Infrastructure;
using CorporateArenasBackend.Models.User;
using CorporateArenasBackend.Repositories.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CorporateArenasBackend.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult<ICollection<User>>> Index()
        {
            return Ok(await _repository.Get());
        }

        [Route(nameof(Create))]
        [HttpPost]
        public async Task<ActionResult<User>> Create(CreateUserRequestModel model)
        {
            var user = await _repository.Create(model);

            return user != null
                ? Created(nameof(Create), user)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }

        [Authorize]
        [Route(nameof(Update))]
        [HttpPost]
        public async Task<ActionResult<User>> Update(UpdateUserRequestModel model)
        {
            var user = await _repository.Update(User.GetId(), model);

            return user != null
                ? Accepted(nameof(Update), model)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }
    }
}