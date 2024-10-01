using CommonLib.Helpers.Interfaces;
using Microsoft.Extensions.Options;
using Moq;
using User.Api.Implements.Factory;
using User.Api.Implements.TokenValidator;
using User.Api.Models;
using User.Api.Models.Responses;
using Xunit;
using static CommonLib.Constants.AppEnums;

namespace User.Api.UnitTest.Implements.TokenValidator
{
    public class FacebookTokenValidatorTest
    {
        private Mock<IOptions<FacebookSetting>>? _mockFacebookSettings;
        private Mock<IApiServices>? _mockApiServices;

        private FacebookTokenValidator CreateService()
        {
            _mockFacebookSettings = new Mock<IOptions<FacebookSetting>>();
            _mockFacebookSettings.Setup(x => x.Value).Returns(new FacebookSetting { AppId = "Fake app id", AppSecret = "Fake app secret" });
            _mockApiServices = new Mock<IApiServices>();
            return new FacebookTokenValidator(
                _mockFacebookSettings.Object,
                _mockApiServices.Object
                );
        }

        [Fact]
        public async Task ValidateToken_Success_ReturnUserInfo()
        {
            //Arrange
            var service = CreateService();
            var userInfo = new ValidateTokenOAuthResponse()
            {
                Email = "Fake user Email",
                Name = "Fake user name"
            };

            _mockApiServices?.Setup(x => x.GetAsync<ValidateTokenOAuthResponse>(It.IsAny<string>())).ReturnsAsync(userInfo);

            //Act
            var result = await service.ValidateToken("validToken");

            // Act & Assert
            Assert.NotNull(result);
            Assert.NotEqual(string.Empty, result.Email);
            Assert.NotEqual(string.Empty, result.Name);
        }

        [Fact]
        public async Task ValidateToken_Failed_ReturnNull()
        {
            //Arrange
            var service = CreateService(); 
            var userInfo = new ValidateTokenOAuthResponse()
            {
                Email = string.Empty,
                Name = string.Empty
            };
            _mockApiServices?.Setup(x => x.GetAsync<ValidateTokenOAuthResponse>(It.IsAny<string>())).ReturnsAsync(userInfo);

            //Act
            var result = await service.ValidateToken("valid token");

            // Act & Assert
            Assert.NotNull(result);
            Assert.Equal(string.Empty, result.Email);
            Assert.Equal(string.Empty, result.Name);
        }
    }
}
