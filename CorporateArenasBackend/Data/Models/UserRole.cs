using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateArenasBackend.Data.Models
{
    public class UserRole
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string RoleId { get; set; }

        public User User { get; set; }

        public Role Role { get; set; }
    }
}
