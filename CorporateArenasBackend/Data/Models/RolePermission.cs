using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Data.Models
{
    public class RolePermission
    {
        public string Id { get; set; }

        public string RoleId { get; set; }

        public string PermissionId { get; set; }

        public Role Role { get; set; }
    }
}
