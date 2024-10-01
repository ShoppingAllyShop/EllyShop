using CommonLib.Helpers.Implements;
using CommonLib.Helpers.Interfaces;
using Microsoft.Extensions.Options;
using User.Api.Implements.Factory;
using User.Api.Implements.TokenValidator;
using User.Api.Models;
using Xunit;
using static CommonLib.Constants.AppEnums;

namespace User.Api.UnitTest.Implements.Factory
{
    public class TokenValidatorFactoryTests
    {
        private ServiceProvider _serviceProvider;
        public TokenValidatorFactoryTests()
        {
            _serviceProvider = UnitTestUtils.CreateServiceProvider();
        }       

        private TokenValidatorFactory CreateService()
        {     
            return new TokenValidatorFactory(
                _serviceProvider
                );
        }

        [Theory]
        [InlineData(AuthProvider.Facebook, typeof(FacebookTokenValidator))]
        [InlineData(AuthProvider.Google, typeof(GoogleTokenValidator))]
        public void CreateValidator_Success_ReturnValidator(AuthProvider provider, Type expectedService)
        {
            //Arrange
            var service = CreateService();
            var result = service.CreateValidator(provider);

            // Assert
            Assert.NotNull(result);
            Assert.IsType(expectedService, result);
        }

        [Fact]
        public void CreateValidator_Failed_InvalidProvider_ThrowArgumentException()
        {
            //Arrange
            var service = CreateService();
            AuthProvider invalidType = (AuthProvider)999;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => service.CreateValidator(invalidType));
        }
    }
}
