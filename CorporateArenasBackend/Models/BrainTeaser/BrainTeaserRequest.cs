using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Models.BrainTeaser
{
    public class BrainTeaserRequest
    {
        [Required]
        public string Riddle { get; set; }
    }
}