using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Models.TrafficUpdate
{
    public class TrafficUpdateRequest
    {
        [Required]
        [MinLength(6)]
        public string Title { get; set; }

        [Required]
        [MinLength(6)]
        public string Body { get; set; }

        [Required(AllowEmptyStrings = false)]
        public bool IsDraft { get; set; }
    }
}