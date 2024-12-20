﻿using AutoMapper;
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
        private readonly IUserContextService _contextAccessor;

        public UserService(IMapper mapper, IPasswordHasher<User> hasher, SocialLensDbContext context, AuthenticationSettings authentication, IUserContextService contextAccessor)
        {
            _mapper = mapper;
            _hasher = hasher;
            _context = context;
            _authentication = authentication;
            _contextAccessor = contextAccessor;
        }
        public async Task RegisterUser(RegisterUserDto dto)
        {
            var addUser = _mapper.Map<User>(dto);

            var hashedPassword = _hasher.HashPassword(addUser, dto.Password);
            addUser.HashPassword = hashedPassword;


            await _context.Users.AddAsync(addUser);
            await _context.SaveChangesAsync();
        }
        public string LogInUser(LogInUserDto dto)
        {
            var user = _context.Users.FirstOrDefault(x => x.Mail == dto.Mail);
            if (user == null)
            {
                throw new BadRequestException("Invalid mail or password");
            }
            var result = _hasher.VerifyHashedPassword(user, user.HashPassword, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid mail or password");
            }
            var claims = new List<Claim>()
            {
                new  Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Mail)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authentication.JWTKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authentication.JWTExpireDays);

            var token = new JwtSecurityToken(_authentication.JWTIssuer,
                _authentication.JWTIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public void DeleteAccount()
        {
            var Id = _contextAccessor.getUserId;
            var User = _context.Users.FirstOrDefault(x => x.Id == Id);
            if(User == null)
            {
                throw new NotFoundException("No account found");
            }
            _context.Users.Remove(User);
            _context.SaveChanges();
            
        }

        public void FollowAccount(int id)
        {
            var Id = _contextAccessor.getUserId;
            var User = _context.Users.FirstOrDefault(x => x.Id == id);
            var User2 = _context.Users.FirstOrDefault(y => y.Id == Id);
            if(User == null || User.BlockedList.Contains(User2.Id))
            {
                throw new NotFoundException("No account found");
            }
            if(User.FollowersList.Contains(User2.Id))
            {
                throw new BadRequestException($"{User.Username} is already followed  by you");
            }
            User2.FollowedList.Add(User.Id);
            User.FollowersList.Add(User2.Id);
            User2.FollowedAmount = User2.FollowedList.Count;
            User.FollowersAmount = User.FollowersList.Count;
            
            _context.SaveChanges();
        }

    }
}
