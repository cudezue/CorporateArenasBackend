using System.Collections.Generic;
using System.Threading.Tasks;
using CorporateArenasBackend.Models.BrainTeaser;
using CorporateArenasBackend.Repositories.BrainTeaser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CorporateArenasBackend.Controllers
{
    public class BrainTeaserController : ApiController
    {
        private static readonly object BrainTeaserNotFound = new {Message = "Brain Teaser not found"};
        private readonly IBrainTeaserRepository _repository;

        public BrainTeaserController(IBrainTeaserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("")]
        public async Task<ActionResult<ICollection<BrainTeaserDto>>> Index()
        {
            return Ok(await _repository.Get());
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<BrainTeaserDto>> Create(BrainTeaserRequest model)
        {
            var brainTeaser = await _repository.Create(model);

            return brainTeaser != null
                ? Created(nameof(Create), brainTeaser)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<BrainTeaserDto>> Update(int id, BrainTeaserRequest model)
        {
            var brainTeaser = await _repository.GetById(id);

            if (brainTeaser == null) return NotFound(BrainTeaserNotFound);

            var result = await _repository.Update(id, model);

            return result != null
                ? Accepted(nameof(Update), result)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }

        [HttpPost]
        [Route("{id}/comment")]
        [AllowAnonymous]
        public async Task<ActionResult<BrainTeaserCommentDto>> AddComment(int id, BrainTeaserCommentRequest model)
        {
            var brainTeaser = await _repository.GetById(id);

            if (brainTeaser == null) return NotFound(BrainTeaserNotFound);

            var result = await _repository.AddComment(id, model);

            return result != null
                ? Created(nameof(AddComment), result)
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