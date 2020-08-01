using CorporateArenasBackend.Models.Permission;
using System.Collections.Generic;

namespace CorporateArenasBackend.Models.Role
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<PermissionDto> Permissions { get; set; }
    }
}