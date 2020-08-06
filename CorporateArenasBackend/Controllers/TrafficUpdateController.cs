using CorporateArenasBackend.Models.TrafficUpdate;
using CorporateArenasBackend.Repositories.TrafficUpdate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Controllers
{
    public class TrafficUpdateController : ApiController
    {
        private readonly ITrafficUpdateRepository _repository;
        private readonly object _trafficUpdateNotFound = new { Message = "Traffic Update not found" };

        public TrafficUpdateController(ITrafficUpdateRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<ICollection<TrafficUpdateDto>>> Index() => Ok(await _repository.GetPublished());

        [HttpGet]
        [Route(nameof(All))]
        public async Task<ActionResult<ICollection<TrafficUpdateDto>>> All() => Ok(await _repository.Get());

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<TrafficUpdateDto>> Create(TrafficUpdateRequest model)
        {
            var trafficUpdate = await _repository.Create(model);

            return trafficUpdate != null
                ? Created(nameof(Create), trafficUpdate)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }

        [HttpGet]
        [Route("{slug}")]
        [AllowAnonymous]
        public async Task<ActionResult<TrafficUpdateDto>> Update(string slug)
        {
            var trafficUpdate = await _repository.GetBySlug(slug);

            if (trafficUpdate == null) return NotFound(_trafficUpdateNotFound);

            return Ok(trafficUpdate);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<TrafficUpdateDto>> Update(int id, TrafficUpdateRequest model)
        {
            var trafficUpdate = await _repository.GetById(id);

            if (trafficUpdate == null) return NotFound(_trafficUpdateNotFound);

            var result = await _repository.Update(trafficUpdate.Id, model);

            return result != null
                ? Accepted(nameof(Update), result)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }
        
        [HttpPost]
        [Route("{id}/comment")]
        [AllowAnonymous]
        public async Task<ActionResult<TrafficUpdateCommentDto>> AddComment(int id, TrafficUpdateCommentRequest model)
        {
            var brainTeaser = await _repository.GetById(id);

            if (brainTeaser == null) return NotFound(_trafficUpdateNotFound);

            var result = await _repository.AddComment(id, model);

            return result != null
                ? Created(nameof(AddComment), result)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<TrafficUpdateDto>> Delete(int id, TrafficUpdateRequest model)
        {
            var trafficUpdate = await _repository.GetById(id);

            if (trafficUpdate == null) return NotFound(_trafficUpdateNotFound);

            await _repository.Delete(trafficUpdate.Id);

            return NoContent();
        }
    }
}