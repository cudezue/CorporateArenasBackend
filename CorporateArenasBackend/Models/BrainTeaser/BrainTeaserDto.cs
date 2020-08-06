using System;
using System.Collections.Generic;

namespace CorporateArenasBackend.Models.BrainTeaser
{
    public class BrainTeaserDto
    {
        public int Id { get; set; }

        public string Riddle { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<BrainTeaserCommentDto> Comments { get; set; }
    }
}