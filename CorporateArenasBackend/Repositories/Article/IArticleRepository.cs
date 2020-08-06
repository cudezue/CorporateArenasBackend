using System.Collections.Generic;
using System.Threading.Tasks;
using CorporateArenasBackend.Models.Article;

namespace CorporateArenasBackend.Repositories.Article
{
    public interface IArticleRepository
    {
        Task<ICollection<ArticleDto>> GetPublished();

        Task<ICollection<ArticleDto>> Get();

        Task<ArticleDto> GetById(int id);

        Task<ArticleDto> GetBySlug(string slug);

        Task<ArticleDto> Create(ArticleRequest model);

        Task<ArticleDto> Update(int id, ArticleRequest model);

        Task<ArticleCommentDto> AddComment(int id, ArticleCommentRequest model);

        Task Delete(int id);
    }
}