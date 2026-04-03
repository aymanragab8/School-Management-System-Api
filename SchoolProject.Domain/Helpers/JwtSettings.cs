namespace SchoolProject.Domain.Helpers
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessTokenExpireMinutes { get; set; }
        public int RefreshTokenExpireDays { get; set; }
    }
}
