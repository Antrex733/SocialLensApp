namespace SocialLensApp.Models
{
    public class RegisterUserDto
    {
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordRepeat { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
