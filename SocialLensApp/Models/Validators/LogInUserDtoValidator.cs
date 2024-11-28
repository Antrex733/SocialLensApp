using FluentValidation;
using SocialLensApp.Data;

namespace SocialLensApp.Models.Validators
{
    public class LogInUserDtoValidator :AbstractValidator<LogInUserDto>
    {
        public LogInUserDtoValidator(SocialLensDbContext dbContext)
        {
            RuleFor(x => x.Mail).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();

        }
        
    }
}
