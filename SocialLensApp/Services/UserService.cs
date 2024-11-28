using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SocialLensApp.Authentication;
using SocialLensApp.Data;
using SocialLensApp.Entities;
using SocialLensApp.Exceptions;
using SocialLensApp.Models;
using SocialLensApp.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace SocialLensApp.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _hasher;
        private readonly SocialLensDbContext _context;
        private readonly AuthenticationSettings _authentication;

        public UserService(IMapper mapper, IPasswordHasher<User> hasher, SocialLensDbContext context, AuthenticationSettings authentication)
        {
            _mapper = mapper;
            _hasher = hasher;
            _context = context;
            _authentication = authentication;
        }
        public async Task RegisterUser(RegisterUserDto dto)
        {
            var addUser = _mapper.Map<User>(dto);

            var hashedPassword = _hasher.HashPassword(addUser, dto.Password);
            addUser.HashPassword = hashedPassword;


            await _context.AddAsync(addUser);
            await _context.SaveChangesAsync();
        }
        public string LogInUser(LogInUserDto dto)
        {
            var user= _context.Users.FirstOrDefault(x => x.Mail==dto.Mail);
            if (user==null)
            {
                throw new BadRequestException("Invalid mail or password");
            }
            var result = _hasher.VerifyHashedPassword(user, user.HashPassword, dto.Password);
            if(result==PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid mail or password");
            }
            var claims = new List<Claim>()
            {
                new  Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Mail)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authentication.JWTKey));
            var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var expires =  DateTime.Now.AddDays(_authentication.JWTExpireDays);

            var token = new JwtSecurityToken(_authentication.JWTIssuer,
                _authentication.JWTIssuer,
                claims,
                expires:  expires,
                signingCredentials:  cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        
    }
}
