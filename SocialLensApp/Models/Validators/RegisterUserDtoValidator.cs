using FluentValidation;
using SocialLensApp.Data;
using System.Text.RegularExpressions;

namespace SocialLensApp.Models.Validators
{
    public class RegisterUserDtoValidator: AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(SocialLensDbContext dbContext)
        {
            RuleFor(x => x.Mail)
                .EmailAddress()
                .NotEmpty();
            RuleFor(x => x.Mail)
                .Custom((value,context) =>
                {
                    var isTaken = dbContext.Users.Any(u => u.Mail == value);
                    if(isTaken)
                    {
                        context.AddFailure("mail","mail is already in use");
                    }
                });
            RuleFor(x => x.Username)
                .Custom((value, context) =>
                {
                    var isTaken = dbContext.Users.Any (u => u.Username == value);
                    if(isTaken)
                    {
                        context.AddFailure("username", "username is already in use");
                    }
                });
            RuleFor(x => x.Password)
                .MinimumLength(8)
                .NotEmpty()
                .Must(password => Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*[\W_]).+$"));


            RuleFor(x => x.PasswordRepeat)
                .Matches(p => p.Password);
               
                
        }
       

    }
}
