using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Data.Models
{
    public class User : IdentityUser
    {
        [MaxLength(191)]
        public string FirstName { get; set; }

        [MaxLength(191)]
        public string LastName { get; set; }

        [MaxLength(191)]
        public string OtherName { get; set; }
        public UserRole Role { get; set; }
    }
}