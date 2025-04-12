using SocialLensApp.Entities;
using SocialLensApp.Models;

namespace SocialLensApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<int> RegisterUser(RegisterUserDto dto);

        string LogInUser(LogInUserDto logInUserDto);

        void DeleteAccount();

        void FollowAccount(int id);

        Task SendFriendRequest(int id);

        Task AcceptFriendRequest(int id);

        List<InviteDto> showFriendRequests();

        void DenyFriendRequest(int id);
        void Block(int id);
        List <int> ShowBLockedList();
        void Unblock(int id);
        void RemoveFriend(int id);
        void Unfollow(int id);
        List<UserDto> SearchUser(string search);
    }


}
