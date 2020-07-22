using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Data.Models
{
    public class Role
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(191)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        public ICollection<UserRole> Users { get; set; }

        public ICollection<RolePermission> Permissions { get; set; }
    }
}
