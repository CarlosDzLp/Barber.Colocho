namespace Barber.Colocho.Models.ForgotPassword
{
    public class ForgotPasswordModel
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
    }
}
