using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorporateArenasBackend.Data;
using CorporateArenasBackend.Data.Models;
using CorporateArenasBackend.Models.BrainTeaser;
using Microsoft.EntityFrameworkCore;

namespace CorporateArenasBackend.Repositories.BrainTeaser
{
    public class BrainTeaserRepository : BaseRepository, IBrainTeaserRepository
    {
        private readonly ApplicationDbContext _db;

        public BrainTeaserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<BrainTeaserDto> Create(BrainTeaserRequest model)
        {
            var brainTeaser = new Data.Models.BrainTeaser
            {
                Riddle = model.Riddle
            };

            _db.BrainTeasers.Add(brainTeaser);
            await _db.SaveChangesAsync();

            return await GetById(brainTeaser.Id);
        }

        public async Task<BrainTeaserCommentDto> AddComment(int id, BrainTeaserCommentRequest model)
        {
            var comment = new BrainTeaserComment
            {
                Body = model.Body,
                Name = model.Name,
                BrainTeaserId = id
            };

            _db.BrainTeaserComments.Add(comment);
            await _db.SaveChangesAsync();

            return new BrainTeaserCommentDto
                {Body = comment.Body, Id = comment.Id, Name = comment.Body, CreatedAt = comment.CreatedAt};
        }

        public async Task Delete(int id)
        {
            var brainTeaser = await _db.BrainTeasers.FindAsync(id);
            _db.BrainTeasers.Remove(brainTeaser);
            await _db.SaveChangesAsync();
        }

        public async Task<ICollection<BrainTeaserDto>> Get()
        {
            return await _db.BrainTeasers
                .Include(brainTeaser => brainTeaser.Comments)
                .Select(brainTeaser => new BrainTeaserDto
                {
                    Id = brainTeaser.Id,
                    Riddle = brainTeaser.Riddle,
                    CreatedAt = brainTeaser.CreatedAt,
                    Comments = brainTeaser.Comments
                        .OrderByDescending(comment => comment.CreatedAt)
                        .Select(comment => new BrainTeaserCommentDto
                        {
                            Id = comment.Id,
                            Name = comment.Name,
                            Body = comment.Body,
                            CreatedAt = comment.CreatedAt
                        }).ToList()
                }).ToListAsync();
        }

        public async Task<BrainTeaserDto> GetById(int id)
        {
            return await _db.BrainTeasers
                .Include(brainTeaser => brainTeaser.Comments)
                .Select(brainTeaser => new BrainTeaserDto
                {
                    Id = brainTeaser.Id,
                    Riddle = brainTeaser.Riddle,
                    CreatedAt = brainTeaser.CreatedAt,
                    Comments = brainTeaser.Comments
                        .OrderByDescending(comment => comment.CreatedAt)
                        .Select(comment => new BrainTeaserCommentDto
                        {
                            Id = comment.Id,
                            Name = comment.Name,
                            Body = comment.Body,
                            CreatedAt = comment.CreatedAt
                        }).ToList()
                }).FirstOrDefaultAsync(brainTeaser => brainTeaser.Id == id);
        }

        public async Task<BrainTeaserDto> Update(int id, BrainTeaserRequest model)
        {
            var brainTeaser = await _db.BrainTeasers.FindAsync(id);

            brainTeaser.Riddle = model.Riddle;

            _db.BrainTeasers.Update(brainTeaser);

            await _db.SaveChangesAsync();

            return await GetById(brainTeaser.Id);
        }
    }
}