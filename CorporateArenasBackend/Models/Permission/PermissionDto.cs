using CorporateArenasBackend.Models.Role;
using System.Collections.Generic;

namespace CorporateArenasBackend.Models.Permission
{
    public class PermissionDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Action { get; set; }

        public string Entity { get; set; }

        public List<RoleDto> Roles { get; set; }
    }
}