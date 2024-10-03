using CommonLib.Helpers.Interfaces;
using Microsoft.Extensions.Options;
using Moq;
using User.Api.Implements.TokenValidator;
using User.Api.Models;
using User.Api.Models.Responses;
using Xunit;

namespace User.Api.UnitTest.Implements.TokenValidator
{
    public class GoogleTokenValidatorTest
    {
        private Mock<IOptions<GoogleSettings>>? _mockGoogleSettings;
        private Mock<IApiServices>? _mockApiServices;

        private GoogleTokenValidator CreateService()
        {
            _mockGoogleSettings = new Mock<IOptions<GoogleSettings>>();
            _mockGoogleSettings.Setup(x => x.Value).Returns(new GoogleSettings { ClientId = "Fake client id" });
            _mockApiServices = new Mock<IApiServices>();
            return new GoogleTokenValidator(
                _mockGoogleSettings.Object,
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
