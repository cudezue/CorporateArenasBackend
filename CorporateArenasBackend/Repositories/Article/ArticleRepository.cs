using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorporateArenasBackend.Data;
using CorporateArenasBackend.Data.Models;
using CorporateArenasBackend.Models.Article;
using CorporateArenasBackend.Utilities;
using Microsoft.EntityFrameworkCore;

namespace CorporateArenasBackend.Repositories.Article
{
    public class ArticleRepository : BaseRepository, IArticleRepository
    {
        private readonly ApplicationDbContext _db;

        public ArticleRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<ArticleDto>> GetPublished()
        {
            return await _db.Articles
                .Where(article => article.PublishedAt != null)
                .OrderByDescending(article => article.PublishedAt)
                .Select(article => new ArticleDto
                {
                    Id = article.Id,
                    Body = article.Body,
                    Title = article.Title,
                    Slug = article.Slug,
                    CreatedAt = article.CreatedAt,
                    PublishedAt = article.PublishedAt,
                    Comments = article.Comments
                        .OrderByDescending(comment => comment.CreatedAt)
                        .Select(comment => new ArticleCommentDto
                        {
                            Body = comment.Body,
                            Id = comment.Id,
                            Name = comment.Name,
                            CreatedAt = comment.CreatedAt
                        }).ToList()
                }).ToListAsync();
        }

        public async Task<ICollection<ArticleDto>> Get()
        {
            return await _db.Articles
                .OrderByDescending(article => article.PublishedAt)
                .Select(article => new ArticleDto
                {
                    Id = article.Id,
                    Body = article.Body,
                    Title = article.Title,
                    Slug = article.Slug,
                    CreatedAt = article.CreatedAt,
                    PublishedAt = article.PublishedAt,
                    Comments = article.Comments
                        .OrderByDescending(comment => comment.CreatedAt)
                        .Select(comment => new ArticleCommentDto
                        {
                            Body = comment.Body,
                            Id = comment.Id,
                            Name = comment.Name,
                            CreatedAt = comment.CreatedAt
                        }).ToList()
                }).ToListAsync();
        }

        public async Task<ArticleDto> GetById(int id)
        {
            return await _db.Articles
                .Select(article => new ArticleDto
                {
                    Id = article.Id,
                    Body = article.Body,
                    Title = article.Title,
                    Slug = article.Slug,
                    CreatedAt = article.CreatedAt,
                    PublishedAt = article.PublishedAt,
                    Comments = article.Comments
                        .OrderByDescending(comment => comment.CreatedAt)
                        .Select(comment => new ArticleCommentDto
                        {
                            Body = comment.Body,
                            Id = comment.Id,
                            Name = comment.Name,
                            CreatedAt = comment.CreatedAt
                        }).ToList()
                })
                .FirstOrDefaultAsync(article => article.Id == id);
        }

        public async Task<ArticleDto> GetBySlug(string slug)
        {
            return await _db.Articles
                .Select(article => new ArticleDto
                {
                    Id = article.Id,
                    Body = article.Body,
                    Title = article.Title,
                    Slug = article.Slug,
                    CreatedAt = article.CreatedAt,
                    PublishedAt = article.PublishedAt,
                    Comments = article.Comments
                        .OrderByDescending(comment => comment.CreatedAt)
                        .Select(comment => new ArticleCommentDto
                        {
                            Body = comment.Body,
                            Id = comment.Id,
                            Name = comment.Name,
                            CreatedAt = comment.CreatedAt
                        }).ToList()
                })
                .FirstOrDefaultAsync(article => article.Slug == slug);
        }

        public async Task<ArticleDto> Create(ArticleRequest model)
        {
            var article = new Data.Models.Article
            {
                Body = model.Body,
                Title = model.Title,
                Slug = UrlHelper.GetFriendlyTitle(model.Title)
            };
            
            if (!model.IsDraft)
                article.PublishedAt = DateTime.UtcNow;

            _db.Articles.Add(article);
            await _db.SaveChangesAsync();

            return await GetById(article.Id);
        }

        public async Task<ArticleDto> Update(int id, ArticleRequest model)
        {
            var article = await _db.Articles.FindAsync(id);

            article.Body = model.Body;
            article.Title = model.Title;
            article.Slug = UrlHelper.GetFriendlyTitle(model.Title);
            
            article.PublishedAt = (DateTime?) (!model.IsDraft ? (object) DateTime.UtcNow : null);

            _db.Articles.Update(article);
            await _db.SaveChangesAsync();

            return await GetById(id);
        }

        public async Task<ArticleCommentDto> AddComment(int id, ArticleCommentRequest model)
        {
            var comment = new ArticleComment
            {
                Body = model.Body,
                Name = model.Name,
                ArticleId = id
            };

            _db.ArticleComments.Add(comment);
            await _db.SaveChangesAsync();

            return new ArticleCommentDto
                {Body = comment.Body, Id = comment.Id, Name = comment.Body, CreatedAt = comment.CreatedAt};
        }

        public async Task Delete(int id)
        {
            var article = await _db.Articles.FindAsync(id);
            _db.Articles.Remove(article);
            await _db.SaveChangesAsync();
        }
    }
}