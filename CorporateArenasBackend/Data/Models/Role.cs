using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CorporateArenasBackend.Data.Models
{
    public class Role : IdentityRole
    {        
        public string Description { get; set; }
        public ICollection<UserRole> Users { get; set; }
        public ICollection<RolePermission> Permissions { get; set; }
    }
}
