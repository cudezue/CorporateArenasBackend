using System;
using System.Collections.Generic;

namespace CorporateArenasBackend.Models.TrafficUpdate
{
    public class TrafficUpdateDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string Slug { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? PublishedAt { get; set; }

        public ICollection<TrafficUpdateCommentDto> Comments { get; set; }
    }
}