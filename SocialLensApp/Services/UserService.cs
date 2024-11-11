using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SocialLensApp.Data;
using SocialLensApp.Entities;
using SocialLensApp.Models;
using SocialLensApp.Services.Interfaces;

namespace SocialLensApp.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _hasher;
        private readonly SocialLensDbContext _context;

        public UserService(IMapper mapper, IPasswordHasher<User> hasher, SocialLensDbContext context)
        {
            _mapper = mapper;
            _hasher = hasher;
            _context = context;
        }
        public async Task RegisterUser(RegisterUserDto dto)
        {
            var addUser = _mapper.Map<User>(dto);
            var hashedPassword = _hasher.HashPassword(addUser, dto.Password);
            addUser.HashPassword = hashedPassword;
            await _context.AddAsync(addUser);
            await _context.SaveChangesAsync();
        }
        

       
    }
}
