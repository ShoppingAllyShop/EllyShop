using CommonLib.Helpers.Interfaces;
using Microsoft.Extensions.Options;
using User.Api.Constant;
using User.Api.Interfaces;
using User.Api.Models;
using User.Api.Models.Responses;

namespace User.Api.Implements.TokenValidator
{
    public class GoogleTokenValidator : ITokenValidator
    {
        private readonly IOptions<GoogleSettings> _googleSettings;
        private readonly IApiServices _apiServices;

        public GoogleTokenValidator(IOptions<GoogleSettings> googleSettings, IApiServices apiServices)
        {
            _googleSettings = googleSettings;
            _apiServices = apiServices;
        }
        public async Task<ValidateTokenOAuthResponse?> ValidateToken(string token)
        {
            var requestUri = string.Format(UserConstant.ValidateTokenGoogleUrl, token);            
            var responseValidate = await _apiServices.GetAsync<ValidateTokenOAuthResponse>(requestUri);
            if (responseValidate == null)
            {
                throw new InvalidOperationException("Invalid google token");
            }
            return responseValidate;
           
        }
    }
}
