using System.Collections.Generic;
using System.Threading.Tasks;
using CorporateArenasBackend.Data.Models;
using CorporateArenasBackend.Repositories.RolePermission;
using Microsoft.AspNetCore.Mvc;

namespace CorporateArenasBackend.Controllers
{
    public class RolePermissionController: ApiController
    {
        private readonly IRolePermissionRepository _repository;

        public RolePermissionController(IRolePermissionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<ICollection<Role>>> Index() => Ok(await _repository.Get());
    }
}