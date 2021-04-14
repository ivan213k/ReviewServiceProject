namespace ReviewService.Application.Common.Models
{
    public class AuthResult
    {
        public bool IsAuthSuccessful { get; set; }
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
    }
}
