using Microsoft.Extensions.Options;
using User.Api.Implements.TokenValidator;
using User.Api.Interfaces;
using User.Api.Interfaces.Factory;
using User.Api.Models;
using static CommonLib.Constants.AppEnums;

namespace User.Api.Implements.Factory
{
    public class TokenValidatorFactory : ITokenValidatorFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public TokenValidatorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public ITokenValidator CreateValidator(AuthProvider provider)
        {

            return provider switch
            {
                AuthProvider.Google => _serviceProvider.GetRequiredService<GoogleTokenValidator>(),
                AuthProvider.Facebook => _serviceProvider.GetRequiredService<FacebookTokenValidator>(),
                _ => throw new ArgumentException("Unknown provider", nameof(provider)),
            };
        }
    }
}
