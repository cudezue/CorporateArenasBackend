using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Models.TrafficUpdate
{
    public class TrafficUpdateCommentRequest
    {
        [Required] public string Name { get; set; }
        [Required] public string Body { get; set; }
    }
}