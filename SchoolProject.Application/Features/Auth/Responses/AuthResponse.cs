namespace SchoolProject.Application.Features.Auth.Responses
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpiresAt { get; set; }
        public DateTime RefreshTokenExpiresAt { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
