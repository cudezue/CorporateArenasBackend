using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Models.User
{
    public class CreateUserRequestModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int Role { get; set; }
    }
}