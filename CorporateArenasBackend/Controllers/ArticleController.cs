using System.Collections.Generic;
using System.Threading.Tasks;
using CorporateArenasBackend.Models.Article;
using CorporateArenasBackend.Repositories.Article;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CorporateArenasBackend.Controllers
{
    public class ArticleController : ApiController
    {
        private static readonly object ArticleNotFound = new {Message = "Article not found"};
        private readonly IArticleRepository _repository;

        public ArticleController(IArticleRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("")]
        public async Task<ActionResult<ICollection<ArticleDto>>> Index()
        {
            return Ok(await _repository.GetPublished());
        }
        
        [HttpGet]
        [AllowAnonymous]
        [Route("{slug}")]
        public async Task<ActionResult<ArticleDto>> FindBySlug(string slug)
        {
            var article = await _repository.GetBySlug(slug);

            return article != null
                ? (ActionResult<ArticleDto>) Ok(article)
                : NotFound(ArticleNotFound);
        }
        
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ArticleDto>> Create(ArticleRequest model)
        {
            var article = await _repository.Create(model);

            return article != null
                ? Created(nameof(Create), article)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }
        
        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<ArticleDto>> Update(int id, ArticleRequest model)
        {
            var article = await _repository.GetById(id);

            if (article == null) return NotFound(ArticleNotFound);

            var result = await _repository.Update(id, model);

            return result != null
                ? Accepted(nameof(Update), result)
                : StatusCode(StatusCodes.Status500InternalServerError, null);
        }

        [HttpPost]
        [Route("{id}/comment")]
        [AllowAnonymous]
        public async Task<ActionResult<ArticleCommentDto>> AddComment(int id, ArticleCommentRequest model)
        {
            var article = await _repository.GetById(id);

            if (article == null) return NotFound(ArticleNotFound);

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