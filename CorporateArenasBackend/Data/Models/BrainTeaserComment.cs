using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Data.Models
{
    public class BrainTeaserComment: Comment
    {
        public int BrainTeaserId { get; set; }

        public BrainTeaser BrainTeaser { get; set; }
    }
}