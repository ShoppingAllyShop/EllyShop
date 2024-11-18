using Comman.Domain.Models;

namespace User.Api.Models.Responses
{
    public class UserResponse
    {
        public Guid UserId { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public RoleModel? Role { get; set; }
    }
}
