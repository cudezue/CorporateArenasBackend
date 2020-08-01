using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Data.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Required] [MaxLength(191)] public string Name { get; set; }
        [Required] public string Description { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<RolePermission> Permissions { get; set; }
    }
}