using CorporateArenasBackend.Infrastructure;
using CorporateArenasBackend.Models.Role;
using CorporateArenasBackend.Repositories.Role;
using CorporateArenasBackend.Repositories.User;
using CorporateArenasBackend.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Controllers
{
    public class RoleController : ApiController
    {
        private readonly IRoleRepository _repository;
        private readonly object _roleNotFound = new { Message = "Role not found" };
        private readonly IUserRepository _userRepository;

        public RoleController(IRoleRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<ICollection<RoleDto>>> Index()
        {
            var hasPermission = !_repository
                .HasPermission(await _userRepository.GetById(User.GetId()), Entities.Role, Actions.Read);

            if (hasPermission)
                return Unauthorized(new { Message = "You are not authorized to perform this action" });

            return Ok(await _repository.Get());
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<RoleDto>> Create(RoleRequestModel model)
        {
            var hasPermission = !_repository
                .HasPermission(await _userRepository.GetById(User.GetId()), Entities.Role, Actions.Create);

            if (hasPermission)
                return Unauthorized(new { Message = "You are not authorized to perform this action" });

            var role = await _repository.Create(model);

            return role != null
                ? Created(nameof(Create), role)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<RoleDto>> Update(int id, RoleRequestModel model)
        {
            var role = await _repository.GetById(id);

            if (role == null) return NotFound(_roleNotFound);

            var hasPermission = !_repository
                .HasPermission(await _userRepository.GetById(User.GetId()), Entities.Role, Actions.Update);

            if (hasPermission)
                return Unauthorized(new { Message = "You are not authorized to perform this action" });

            var result = await _repository.Update(role.Id, model);

            return result != null
                ? Accepted(nameof(Update), result)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var role = await _repository.GetById(id);

            if (role == null) return NotFound(_roleNotFound);

            var hasPermission = !_repository
                .HasPermission(await _userRepository.GetById(User.GetId()), Entities.Role, Actions.Delete);

            if (hasPermission)
                return Unauthorized(new { Message = "You are not authorized to perform this action" });

            await _repository.Delete(id);

            return NoContent();
        }
    }
}