using System.Collections.Generic;
using System.Threading.Tasks;
using CorporateArenasBackend.Data.Models;
using CorporateArenasBackend.Models.Permission;
using CorporateArenasBackend.Repositories.Permission;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CorporateArenasBackend.Controllers
{
    public class PermissionController : ApiController
    {
        private static readonly object PermissionNotFound = new {Message = "Permission not found"};
        private readonly IPermissionRepository _repository;

        public PermissionController(IPermissionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<ICollection<Permission>>> Index()
        {
            return Ok(await _repository.Get());
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Permission>> Create(PermissionRequestModel model)
        {
            var permission = await _repository.Create(model);

            return permission != null
                ? Created(nameof(Create), permission)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<Permission>> Update(string id, PermissionRequestModel model)
        {
            var permission = await _repository.GetById(id);

            if (permission == null) return NotFound(PermissionNotFound);

            var result = await _repository.Update(permission.Id, model);

            return result != null
                ? Accepted(nameof(Update), result)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var permission = await _repository.GetById(id);

            if (permission == null) return NotFound(PermissionNotFound);

            await _repository.Delete(permission);

            return NoContent();
        }
    }
}