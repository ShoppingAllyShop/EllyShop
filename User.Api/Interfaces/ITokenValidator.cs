using User.Api.Models.Responses;

namespace User.Api.Interfaces
{
    public interface ITokenValidator
    {
        Task<ValidateTokenOAuthResponse?> ValidateToken(string token);
    }
}
