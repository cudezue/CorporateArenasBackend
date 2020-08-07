using System.Collections.Generic;
using System.Threading.Tasks;
using CorporateArenasBackend.Models.Vacancy;
using CorporateArenasBackend.Repositories.Vacancy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CorporateArenasBackend.Controllers
{
    public class VacancyController : ApiController
    {
        private static readonly object VacancyNotFound = new {Message = "Vacancy not found"};
        private readonly IVacancyRepository _repository;

        public VacancyController(IVacancyRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<ICollection<VacancyDto>>> Index()
        {
            return Ok(await _repository.Get());
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<VacancyDto>> GetById(int id)
        {
            var vacancy = await _repository.GetById(id);

            if (vacancy == null) return NotFound(VacancyNotFound);

            return Ok(vacancy);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<VacancyDto>> Create(VacancyRequest model)
        {
            var vacancy = await _repository.Create(model);

            return vacancy != null
                ? Created(nameof(Create), vacancy)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<VacancyDto>> Update(int id, VacancyRequest model)
        {
            var vacancy = await _repository.GetById(id);

            if (vacancy == null) return NotFound(VacancyNotFound);

            var result = await _repository.Update(id, model);

            return result != null
                ? Accepted(nameof(Update), result)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }
        
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _repository.Delete(id);

            return NoContent();
        }
    }
}