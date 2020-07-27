using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Models.Permission
{
    public class PermissionRequestModel
    {
        [Required] public string Name { get; set; }

        [Required] public string Description { get; set; }

        [Required] [MaxLength(191)] public string Action { get; set; }

        [Required] [MaxLength(191)] public string Entity { get; set; }
    }
}