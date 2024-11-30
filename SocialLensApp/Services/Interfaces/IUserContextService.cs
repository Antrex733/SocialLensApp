using System.Security.Claims;

namespace SocialLensApp.Services.Interfaces
{
    public interface IUserContextService
    {
        public ClaimsPrincipal User { get; }
        public int? getUserId {  get;  }
    }
}
