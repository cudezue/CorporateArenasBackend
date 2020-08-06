using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Models.BrainTeaser
{
    public class BrainTeaserCommentRequest
    {
        [Required] public string Name { get; set; }
        [Required] public string Body { get; set; }
    }
}