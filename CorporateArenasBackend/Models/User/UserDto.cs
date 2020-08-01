using CorporateArenasBackend.Models.Role;

namespace CorporateArenasBackend.Models.User
{
    public class UserDto
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string OtherName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public RoleDto Role { get; set; }
    }
}