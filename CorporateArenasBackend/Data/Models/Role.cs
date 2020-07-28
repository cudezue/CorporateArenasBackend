﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CorporateArenasBackend.Data.Models
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }
        public IList<UserRole> Users { get; set; }
        public IList<RolePermission> Permissions { get; set; }
    }
}