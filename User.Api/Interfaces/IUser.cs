using User.Api.Models.Requests;
using User.Api.Models.Responses;

namespace User.Api.Interfaces
{
    public interface IUser
    {
        string GenerateAccessToken(string email, string roleName);
        Task<UserResponse> HandleSocialLoginAsync(SocialLoginRequest request);
        Task<string> RefreshTokenAsync(RefreshTokenRequestModel model);
        Task<UserResponse> HandleLoginAsync(UserAuthRequest model);
        Task<UserResponse?> CreateAccountAsync(UserAuthRequest model);
        Task<SearchEmployeeUserResponse> SearchEmployeeUserAsync(int? pageNumber, int? pageSize, string? sortBy, string? sortOrder, string? searchInput);
        Task<DataAdminUserPageResponse> GetDataAdminUserPageAsync();
        Task<string> UpdateUserAsync(UserAuthRequest request);
        Task<DeleteUserResponse> DeleteUserAsync(DeleteUserRequest model);
    }
}
