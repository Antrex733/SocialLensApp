namespace SocialLensApp.Authentication
{
    public class AuthenticationSettings
    {
        public string? JWTKey { get; set; }
        public int JWTExpireDays { get; set; }
        public string? JWTIssuer { get; set; }
    }
}
