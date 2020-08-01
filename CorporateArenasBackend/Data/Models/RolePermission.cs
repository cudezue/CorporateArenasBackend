using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Data.Models
{
    public class RolePermission
    {
        [Required] public int RoleId { get; set; }

        [Required] public int PermissionId { get; set; }

        public Role Role { get; set; }

        public Permission Permission { get; set; }
    }
}