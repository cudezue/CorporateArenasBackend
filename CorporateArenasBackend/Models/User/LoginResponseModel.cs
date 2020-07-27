namespace CorporateArenasBackend.Models.User
{
    public class LoginResponseModel
    {
        public string Token { get; set; }

        public Data.Models.User User { get; set; }
    }
}