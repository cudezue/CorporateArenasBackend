using CorporateArenasBackend.Data;
using CorporateArenasBackend.Models.BrainTeaser;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task Delete(int id)
        {
            var brainTeaser = await _db.BrainTeasers.FindAsync(id);
            _db.BrainTeasers.Remove(brainTeaser);
            await _db.SaveChangesAsync();
        }

        public async Task<ICollection<BrainTeaserDto>> Get() => await _db.BrainTeasers
            .Select(brainTeaser => new BrainTeaserDto
            {
                Id = brainTeaser.Id,
                Riddle = brainTeaser.Riddle,
                CreatedAt = brainTeaser.CreatedAt
            }).ToListAsync();

        public async Task<BrainTeaserDto> GetById(int id) => await _db.BrainTeasers
            .Select(brainTeaser => new BrainTeaserDto
            {
                Id = brainTeaser.Id,
                Riddle = brainTeaser.Riddle,
                CreatedAt = brainTeaser.CreatedAt
            }).FirstOrDefaultAsync(brainTeaser => brainTeaser.Id == id);

        public async Task<BrainTeaserDto> Update(int id, BrainTeaserRequest model)
        {
            var brainTeaser = await _db.BrainTeasers.FindAsync(id);

            brainTeaser.Riddle = model.Riddle;

            await _db.SaveChangesAsync();

            return await GetById(brainTeaser.Id);
        }
    }
}