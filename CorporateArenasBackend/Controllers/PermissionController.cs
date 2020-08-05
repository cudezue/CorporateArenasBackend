using CorporateArenasBackend.Data.Models;
using CorporateArenasBackend.Infrastructure;
using CorporateArenasBackend.Models.Permission;
using CorporateArenasBackend.Repositories.Permission;
using CorporateArenasBackend.Repositories.User;
using CorporateArenasBackend.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Controllers
{
    public class PermissionController : ApiController
    {
        private static readonly object PermissionNotFound = new { Message = "Permission not found" };
        private readonly IPermissionRepository _repository;
        private readonly IUserRepository _userRepository;

        public PermissionController(IPermissionRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<ICollection<PermissionDto>>> Index()
        {
            var hasPermission = !_repository
               .HasPermission(await _userRepository.GetById(User.GetId()), Entities.Permission, Actions.Read);

            if (hasPermission)
                return Unauthorized(new { Message = "You are not authorized to perform this action" });

            return Ok(await _repository.Get());
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Permission>> Create(PermissionRequestModel model)
        {
            var hasPermission = !_repository
                .HasPermission(await _userRepository.GetById(User.GetId()), Entities.Permission, Actions.Create);

            if (hasPermission)
                return Unauthorized(new { Message = "You are not authorized to perform this action" });

            var permission = await _repository.Create(model);

            return permission != null
                ? Created(nameof(Create), permission)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<PermissionDto>> Update(int id, PermissionRequestModel model)
        {
            var permission = await _repository.GetById(id);

            if (permission == null) return NotFound(PermissionNotFound);

            var hasPermission = !_repository
                .HasPermission(await _userRepository.GetById(User.GetId()), Entities.Permission, Actions.Update);

            if (hasPermission)
                return Unauthorized(new { Message = "You are not authorized to perform this action" });

            var result = await _repository.Update(permission.Id, model);

            return result != null
                ? Accepted(nameof(Update), result)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var permission = await _repository.GetById(id);

            if (permission == null) return NotFound(PermissionNotFound);

            var hasPermission = !_repository
                .HasPermission(await _userRepository.GetById(User.GetId()), Entities.Permission, Actions.Delete);

            if (hasPermission)
                return Unauthorized(new { Message = "You are not authorized to perform this action" });

            await _repository.Delete(id);

            return NoContent();
        }
    }
}