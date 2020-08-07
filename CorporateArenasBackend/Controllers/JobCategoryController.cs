using System.Collections.Generic;
using System.Threading.Tasks;
using CorporateArenasBackend.Models.Vacancy;
using CorporateArenasBackend.Repositories.Vacancy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CorporateArenasBackend.Controllers
{
    public class JobCategoryController : ApiController
    {
        private static readonly object JobCategoryNotFound = new {Message = "Job Category not found"};
        private readonly IJobCategoryRepository _repository;

        public JobCategoryController(IJobCategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<ICollection<JobCategoryDto>>> Index()
        {
            return Ok(await _repository.Get());
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<JobCategoryDto>> GetById(int id)
        {
            var jobCategory = await _repository.GetById(id);

            if (jobCategory == null) return NotFound(JobCategoryNotFound);

            return Ok(jobCategory);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<JobCategoryDto>> Create(JobCategoryRequest model)
        {
            var jobCategory = await _repository.Create(model);

            return jobCategory != null
                ? Created(nameof(Create), jobCategory)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<JobCategoryDto>> Update(int id, JobCategoryRequest model)
        {
            var jobCategory = await _repository.Update(id, model);

            return jobCategory != null
                ? Accepted(nameof(Update), jobCategory)
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