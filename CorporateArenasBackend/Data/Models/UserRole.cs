﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Data.Models
{
    public class UserRole
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string RoleId { get; set; }

        public User User { get; set; }

        public Role Role { get; set; }
    }
}