using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SocialLensApp.Authentication;
using SocialLensApp.Data;
using SocialLensApp.Entities;
using SocialLensApp.Exceptions;
using SocialLensApp.Migrations;
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
        public async Task<int> RegisterUser(RegisterUserDto dto)
        {
            var addUser = _mapper.Map<User>(dto);

            var hashedPassword = _hasher.HashPassword(addUser, dto.Password);
            addUser.HashPassword = hashedPassword;


            await _context.Users.AddAsync(addUser);
            await _context.SaveChangesAsync();
            return addUser.Id;
        }
        public string LogInUser(LogInUserDto dto)
        {
            var x = _context.Users;
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
        
        public void Unfollow(int id)
        {
            var UserId = _contextAccessor.getUserId;
            var User = _context.Users.FirstOrDefault(i => i.Id == UserId);
            var User2 = _context.Users.FirstOrDefault(x => x.Id == id);
            User.FollowedList.Remove(id);
            User.FollowedAmount -= 1;
            User2.FollowersList.Remove(User.Id);
            User2.FollowersAmount -= 1;
            _context.SaveChanges();
        }

        public async Task SendFriendRequest(int id)
        {
            var Id = _contextAccessor.getUserId;
            var User = _context.Users.FirstOrDefault(x => x.Id == id);
            var User2 = _context.Users.FirstOrDefault(y => y.Id == Id);
            if (User == null || User.BlockedList.Contains(User2.Id))
            {
                throw new NotFoundException("No account found");
            }
            if(User2.FriendsList.Contains(id) ) 
            {
                throw new Exception($"{User.Username} is already your friend");
            }
            
            if (_context.Invites.FirstOrDefault(x => x.UserId == Id && x.InvitingUser==User.Id) != null  )
            {
                throw new Exception($"You have already sent an invite to {User.Username}");
            }
            var Invite = new Invite() { InvitingUser = User.Id, UserId= User2.Id};

            _context.Add(Invite);
            await _context.SaveChangesAsync();
            
        }

        public List<InviteDto> showFriendRequests()
        {
            var id = _contextAccessor.getUserId;
            var inviteDtos = _context.Invites

        .Where(invite => invite.InvitingUser == id && !_context.Users.First(u => u.Id == id).BlockedList.Contains(invite.UserId));  // Find invites sent by this user
        var invites = inviteDtos.Join(_context.Users,
              invite => invite.UserId, // Match Invites.UserId (the recipient)
              user => user.Id,         // To Users.Id
              (invite, user) => new InviteDto
              {
                  InvitingUserId = invite.UserId, // Include the ID
                  Username = user.Username, // Include the username
                  InviteId = invite.Id
              })
        .ToList();

            return invites;
        }
        public void DenyFriendRequest(int id)
        {
            var UserId = _contextAccessor.getUserId;
            var invite = _context.Invites.FirstOrDefault(i => i.Id == id && i.InvitingUser == UserId );
            if (invite == null)
                throw new Exception("No invite has been found");


            _context.Remove(invite);
            _context.SaveChanges();
        }
        public Task AcceptFriendRequest(int id)
        {
            var UserId = _contextAccessor.getUserId;
            var invite = _context.Invites.FirstOrDefault(i => i.Id == id && i.InvitingUser == UserId);
            if (invite == null)
                throw new Exception("No invite has been found");

            var User = _context.Users.FirstOrDefault(i => i.Id == UserId);
            User.FriendsList.Add(invite.UserId);
            var User2 = _context.Users.FirstOrDefault(i => i.Id == invite.UserId);
            User2.FriendsList.Add(invite.InvitingUser);
            _context.Remove(invite);
            User.FriendsAmount += 1;
            User2.FriendsAmount += 1;
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public void RemoveFriend(int id)
        {
            var UserId = _contextAccessor.getUserId;
            var User = _context.Users.FirstOrDefault(i => i.Id == UserId);
            var User2 = _context.Users.FirstOrDefault(x => x.Id == id);
            User.FriendsList.Remove(id);
            User.FriendsAmount -= 1;
            User2.FriendsList.Remove(User.Id);
            User2.FriendsAmount -= 1;
            _context.SaveChanges();
        }

        public void Block(int id)
        {
            var UserId = _contextAccessor.getUserId;
            var User = _context.Users.FirstOrDefault(i => i.Id == UserId);
            if (User.BlockedList.Contains(id))
                throw new Exception("User is already blocked");
            User.BlockedList.Add(id);
            _context.SaveChanges();
        }

        public  List<int> ShowBLockedList()
        {
            var Id = _contextAccessor.getUserId;
            var User = _context.Users.FirstOrDefault(i => i.Id == Id);
            var BlockedList = User.BlockedList;


            return BlockedList;
        }
        
        public void Unblock(int id)
        {
            var UserId = _contextAccessor.getUserId;
            var User = _context.Users.FirstOrDefault(i => i.Id == UserId);
            var x = User.BlockedList.FirstOrDefault(i => i == id);
            if (x==default)
                throw new Exception("User is not blocked");
            User.BlockedList.Remove(x);

            _context.SaveChanges();
        }

        public List<UserDto> SearchUser(string search)
        {
            var Users = _context.Users.Where(i => i.Name.ToLower().Contains(search.ToLower()) || i.Username.ToLower().Contains(search.ToLower()));
            var UserDto= _mapper.Map<List<UserDto>>(Users);
            return UserDto;
        }
    }
}
