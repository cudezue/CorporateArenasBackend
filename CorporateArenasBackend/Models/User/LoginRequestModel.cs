using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Models.User
{
    public class LoginRequestModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}