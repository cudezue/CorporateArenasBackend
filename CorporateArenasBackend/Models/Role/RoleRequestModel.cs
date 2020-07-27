using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Models.Role
{
    public class RoleRequestModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
    }
}