using System.ComponentModel.DataAnnotations;

namespace CorporateArenasBackend.Models.User
{
    public class UpdateUserRequestModel
    {
        [Required]
        [MaxLength(191)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(191)]
        public string LastName { get; set; }
        
        [MaxLength(191)]
        public string OtherName { get; set; }
        
        [Required]
        [MaxLength(14)]
        public string PhoneNumber { get; set; }
    }
}