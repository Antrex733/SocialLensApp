namespace SocialLensApp.Models
{
    public class RegisterUserDto
    {
        public string Mail { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string PasswordRepeat { get; set; } = default!;
        public DateTime DateOfBirth { get; set; } = default!;
    }
}
