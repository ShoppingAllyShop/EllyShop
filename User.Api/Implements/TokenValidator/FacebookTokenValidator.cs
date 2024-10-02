using CommonLib.Helpers.Interfaces;
using Microsoft.Extensions.Options;
using User.Api.Constant;
using User.Api.Interfaces;
using User.Api.Models;
using User.Api.Models.Responses;

namespace User.Api.Implements.TokenValidator
{
    public class FacebookTokenValidator : ITokenValidator
    {        
        private readonly IOptions<FacebookSetting> _facebookSettings;
        private readonly IApiServices _apiServices;

        public FacebookTokenValidator(IOptions<FacebookSetting> facebookSettings, IApiServices apiServices)
        {
            _apiServices = apiServices;
            _facebookSettings = facebookSettings;
        }
        public async Task<ValidateTokenOAuthResponse?> ValidateToken(string token)
        {            
            var validateTokenFacebookUrl = string.Format(UserConstant.ValidateTokenFacebookUrl, token);
            var validateTokenResult = await _apiServices.GetAsync<ValidateTokenOAuthResponse>(validateTokenFacebookUrl);
            return validateTokenResult;
        }
    }
}
