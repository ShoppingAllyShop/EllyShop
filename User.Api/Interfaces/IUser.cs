using Comman.Domain.Models;
using User.Api.Models.Requests;
using User.Api.Models.Responses;
using static CommonLib.Constants.AppEnums;

namespace User.Api.Interfaces
{
    public interface IUser
    {
        string GenerateAccessToken(string email, UserRoleEnum role);
        Task<UserResponse> HandleSocialLogin(SocialLoginRequest request);
        Task<string> RefreshToken(RefreshTokenRequestModel model);
        Task<UserResponse> HandleLoginAsync(UserAuthRequest model);
        Task<UserResponse?> CreateAccount(UserAuthRequest model);
    }
}
