using SocialLensApp.Services.Interfaces;
using System.Security.Claims;

namespace SocialLensApp.Services
{
    public class UserContextService : IUserContextService
    {
        private IHttpContextAccessor _contextAccessor;

        public UserContextService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public ClaimsPrincipal User => _contextAccessor.HttpContext?.User;
        public int? getUserId => User == null? null : int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);

      
    }
}
