using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Data.Models
{
    public class BrainTeaser
    {
        public int Id { get; set; }

        [Required] public string Riddle { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<BrainTeaserComment> Comments { get; set; }
    }
}