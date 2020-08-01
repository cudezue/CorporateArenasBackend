using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Data.Models
{
    public class Permission
    {
        public int Id { get; set; }

        [Required] [MaxLength(191)] public string Name { get; set; }

        [Required] public string Description { get; set; }

        [Required] [MaxLength(191)] public string Action { get; set; }

        [Required] [MaxLength(191)] public string Entity { get; set; }

        public ICollection<RolePermission> Roles { get; set; }
    }
}