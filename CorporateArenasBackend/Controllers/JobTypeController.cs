using CorporateArenasBackend.Models.Vacancy;
using CorporateArenasBackend.Repositories.Vacancy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Controllers
{
    public class JobTypeController: ApiController
    {
        private static readonly object JobTypeNotFound = new { Message = "Job Type not found" };
        private readonly IJobTypeRepository _repository;

        public JobTypeController(IJobTypeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("")]
        public async Task<ActionResult<ICollection<JobTypeDto>>> Index()
        {
            return Ok(await _repository.Get());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{id}")]
        public async Task<ActionResult<JobTypeDto>> FindById(int id)
        {
            var jobType = await _repository.GetById(id);

            return jobType != null
                ? (ActionResult<JobTypeDto>)Ok(jobType)
                : NotFound(JobTypeNotFound);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<JobTypeDto>> Create(JobTypeRequest model)
        {
            var jobType = await _repository.Create(model);

            return jobType != null
                ? Created(nameof(Create), jobType)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<JobTypeDto>> Update(int id, JobTypeRequest model)
        {
            var jobType = await _repository.GetById(id);

            if (jobType == null) return NotFound(JobTypeNotFound);

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
