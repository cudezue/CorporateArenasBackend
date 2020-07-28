using System.Collections.Generic;
using System.Threading.Tasks;
using CorporateArenasBackend.Data.Models;
using CorporateArenasBackend.Models.Role;
using CorporateArenasBackend.Repositories.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CorporateArenasBackend.Controllers
{
    [Authorize]
    public class RoleController : ApiController
    {
        private readonly IRoleRepository _repository;
        private readonly object _roleNotFound = new {Message = "Role not found"};

        public RoleController(IRoleRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<ICollection<Role>>> Index()
        {
            return Ok(await _repository.Get());
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Role>> Create(RoleRequestModel model)
        {
            var role = await _repository.Create(model);

            return role != null
                ? Created(nameof(Create), role)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<Role>> Update(string id, RoleRequestModel model)
        {
            var role = await _repository.GetById(id);

            if (role == null) return NotFound(_roleNotFound);

            var result = await _repository.Update(role.Id, model);

            return result != null
                ? Accepted(nameof(Update), result)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var role = await _repository.GetById(id);

            if (role == null) return NotFound(_roleNotFound);

            await _repository.Delete(role);

            return NoContent();
        }
    }
}