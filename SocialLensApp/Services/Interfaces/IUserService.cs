using SocialLensApp.Models;

namespace SocialLensApp.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterUser(RegisterUserDto dto);

        string LogInUser(LogInUserDto logInUserDto);
        
    }

    
}
