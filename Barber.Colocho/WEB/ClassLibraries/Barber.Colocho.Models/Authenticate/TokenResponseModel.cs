namespace Barber.Colocho.Models.Authenticate
{
    public class TokenResponseModel
    {
        public int UserId { get; set; }
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
