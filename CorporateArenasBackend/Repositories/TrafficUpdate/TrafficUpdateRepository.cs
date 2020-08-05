using CorporateArenasBackend.Data;
using CorporateArenasBackend.Models.TrafficUpdate;
using CorporateArenasBackend.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Repositories.TrafficUpdate
{
    public class TrafficUpdateRepository : BaseRepository, ITrafficUpdateRepository
    {
        private readonly ApplicationDbContext _db;

        public TrafficUpdateRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<TrafficUpdateDto> Create(TrafficUpdateRequest model)
        {
            var trafficUpdate = new Data.Models.TrafficUpdate
            {
                Title = model.Title,
                Body = model.Body,
                Slug = UrlHelper.GetFriendlyTitle(model.Title)
            };

            if (!model.IsDraft)
                trafficUpdate.PublishedAt = DateTime.UtcNow;

            _db.TrafficUpdates.Add(trafficUpdate);
            await _db.SaveChangesAsync();

            return await GetById(trafficUpdate.Id);
        }

        public async Task Delete(int id)
        {
            var trafficUpdate = await _db.TrafficUpdates.FindAsync(id);
            _db.TrafficUpdates.Remove(trafficUpdate);
            await _db.SaveChangesAsync();
        }

        public async Task<ICollection<TrafficUpdateDto>> GetPublished() => await _db.TrafficUpdates
                .Where(trafficUpdate => trafficUpdate.PublishedAt != null)
            .OrderByDescending(trafficUpdate => trafficUpdate.PublishedAt)
                .Select(trafficUpdate => new TrafficUpdateDto
                {
                    Id = trafficUpdate.Id,
                    Body = trafficUpdate.Body,
                    Title = trafficUpdate.Title,
                    Slug = trafficUpdate.Slug,
                    CreatedAt = trafficUpdate.CreatedAt,
                    PublishedAt = trafficUpdate.PublishedAt
                }).ToListAsync();

        public async Task<TrafficUpdateDto> GetById(int id) => await _db.TrafficUpdates
            .Select(trafficUpdate => new TrafficUpdateDto
            {
                Id = trafficUpdate.Id,
                Body = trafficUpdate.Body,
                Title = trafficUpdate.Title,
                Slug = trafficUpdate.Slug,
                CreatedAt = trafficUpdate.CreatedAt,
                PublishedAt = trafficUpdate.PublishedAt
            })
            .FirstOrDefaultAsync(trafficUpdate => trafficUpdate.Id == id);

        public async Task<TrafficUpdateDto> Update(int id, TrafficUpdateRequest model)
        {
            var trafficUpdate = await _db.TrafficUpdates.FindAsync(id);

            trafficUpdate.Title = model.Title;
            trafficUpdate.Body = model.Body;
            trafficUpdate.Slug = UrlHelper.GetFriendlyTitle(model.Title);

            trafficUpdate.PublishedAt = (DateTime?)(!model.IsDraft ? (object)DateTime.UtcNow : null);

            _db.TrafficUpdates.Update(trafficUpdate);
            await _db.SaveChangesAsync();

            return await GetById(trafficUpdate.Id);
        }

        public async Task<ICollection<TrafficUpdateDto>> Get() => await _db.TrafficUpdates
                .Select(trafficUpdate => new TrafficUpdateDto
                {
                    Id = trafficUpdate.Id,
                    Body = trafficUpdate.Body,
                    Title = trafficUpdate.Title,
                    Slug = trafficUpdate.Slug,
                    CreatedAt = trafficUpdate.CreatedAt,
                    PublishedAt = trafficUpdate.PublishedAt
                }).ToListAsync();

        public async Task<TrafficUpdateDto> GetBySlug(string slug) => await _db.TrafficUpdates
            .Select(trafficUpdate => new TrafficUpdateDto
            {
                Id = trafficUpdate.Id,
                Body = trafficUpdate.Body,
                Title = trafficUpdate.Title,
                Slug = trafficUpdate.Slug,
                CreatedAt = trafficUpdate.CreatedAt,
                PublishedAt = trafficUpdate.PublishedAt
            })
            .FirstOrDefaultAsync(trafficUpdate => trafficUpdate.Slug == slug);
    }
}