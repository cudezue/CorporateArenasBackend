namespace CorporateArenasBackend.Models.User
{
    public class LoginResponseModel
    {
        public string Token { get; set; }

        public UserDto User { get; set; }
    }
}