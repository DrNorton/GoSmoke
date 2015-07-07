namespace GoSmokeBackend.Controllers.Models
{
    public class ResetPasswordModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
    }
}
