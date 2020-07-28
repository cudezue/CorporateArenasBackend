using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Data.Models
{
    public class RolePermission
    {
        public int Id { get; set; }

        [Required] public string RoleId { get; set; }

        [Required] public int PermissionId { get; set; }

        public Role Role { get; set; }
    }
}