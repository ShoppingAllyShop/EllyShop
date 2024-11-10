using Comman.Domain.Models;
using Common.Infrastructure.Interfaces;
using CommonLib.Helpers.Implements;
using CommonLib.Helpers.Interfaces;
using CommonLib.Models.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System.Linq.Expressions;
using System.Net.Mail;
using User.Api.Implements;
using User.Api.Implements.TokenValidator;
using User.Api.Interfaces.Factory;
using User.Api.Models;
using User.Api.Models.Requests;
using User.Api.Models.Responses;
using Xunit;
using Xunit.Sdk;
using static CommonLib.Constants.AppEnums;
using CommonLib.TestHelpers;
using System.Reflection.Metadata.Ecma335;
using User.Api.Interfaces;
using Microsoft.VisualBasic;
using CommonLib.Enums;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace User.Api.UnitTest.Implements
{
    public class UserServicesTest
    {
        private Mock<IOptions<AuthenticationSetting>> _mockAuthenticationSettings = new Mock<IOptions<AuthenticationSetting>>();
        private Mock<IOptions<FacebookSetting>> _mockFacebookSettings = new Mock<IOptions<FacebookSetting>>();
        private Mock<ILogger<UserServices>> _mockLogger = new Mock<ILogger<UserServices>>();
        private Mock<IUnitOfWork<EllyShopContext>> _mockUnitOfWork = new Mock<IUnitOfWork<EllyShopContext>>();
        private Mock<IApiServices> _mockApiServices = new Mock<IApiServices>();
        private Mock<ITokenValidatorFactory> _mockValidatorFactory = new Mock<ITokenValidatorFactory>();
        private Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
        private ServiceProvider? _serviceProvider;

        private readonly string Mail1 = "Test1@gmail.com";
        private readonly string Mail2 = "Test2@gmail.com";
        private readonly string PassHash1 = "AvN8e2kj9PwpW/Qo56eCkA==:0Fz0jQa0hloGaqWQ0ydE0hPM6LY=";
        private readonly string PassInput1 = "123456";
        private readonly string RefreshToken1 = "JSNtfseRpkkj3m59bVe1cJN4444/mTLRTVIwLl5w4c=";
        private readonly string RefreshToken2 = "JSNtfseRpkkj3m59bVe1cJN5555/mTLRTVIwLl5w4c=";
        private readonly Guid UserId1 = new Guid("af24e5b0-a03d-4301-b54d-2229867604a9");
        private readonly Guid UserId2 = new Guid("af24e5b0-a03d-4301-b54d-2229867604a8");

        private readonly Guid AdminRoleId = new Guid("5A387B6E-1B29-47E1-84F6-D25D4287B11C");
        private readonly Guid CustomerRoleId = new Guid("87A989E6-C641-4222-AF8B-9CFC3C9B1183");
        private readonly string AuthenSecretKey = "FakeSecretKey1111111111111111111";

        public UserServicesTest()
        {

        }

        private UserServices CreateService()
        {
            return new UserServices(
                _mockAuthenticationSettings.Object,
                _mockFacebookSettings.Object,
                _mockLogger.Object,
                _mockUnitOfWork.Object,
                _mockApiServices.Object,
                _mockValidatorFactory.Object);
        }

        #region RefreshToken
        [Fact]
        public async Task RefreshToken_Success_ReturnAccessToken()
        {
            //Arrange
            var service = CreateService();
            var refreshTokenData = CreateRefreshTokenData();
            var authenAppSetttings = CreateAuthenAppSettingData();
            var requestModel = new RefreshTokenRequestModel
            {
                Email = Mail1,
                RefreshToken = RefreshToken1,
                Role = new RoleModel
                {
                    RoleId = Guid.NewGuid(),
                    RoleName = UserRoleEnum.Customer.GetEnumDescription()
                }
            };

            _mockAuthenticationSettings.Setup(x => x.Value).Returns(authenAppSetttings);
            _mockUnitOfWork.Setup(x => x.Repository<RefreshTokens>()).Returns(CreateRefreshTokenData().MockDbSet().Object);

            //Act
            var result = await service.RefreshToken(requestModel);

            //Assert
            Assert.NotEqual(string.Empty, result);
        }

        [Fact]
        public async Task RefreshToken_Failed_StoredTokenNull_ReturnStringEmpty()
        {
            //Arrange
            var service = CreateService();
            var refreshTokenData = CreateRefreshTokenData();
            var requestModel = new RefreshTokenRequestModel
            {
                Email = Mail2,
                RefreshToken = "RefreshToken2",
            };

            _mockUnitOfWork.Setup(x => x.Repository<RefreshTokens>()).Returns(CreateRefreshTokenData().MockDbSet().Object);

            //Act
            var result = await service.RefreshToken(requestModel);

            //Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public async Task RefreshToken_Failed_InvalidRefreshToken_ReturnStringEmpty()
        {
            //Arrange
            var service = CreateService();
            var refreshTokenData = CreateRefreshTokenData();
            var requestModel = new RefreshTokenRequestModel
            {
                Email = Mail2,
                RefreshToken = RefreshToken2,
            };

            _mockUnitOfWork.Setup(x => x.Repository<RefreshTokens>()).Returns(CreateRefreshTokenData().MockDbSet().Object);

            //Act
            var result = await service.RefreshToken(requestModel);

            //Assert
            Assert.Equal(string.Empty, result);
        }
        #endregion

        #region GenerateAccessToken
        [Theory]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData("", " ")]
        [InlineData(" ", " ")]
        public void GenerateAccessToken_Failed_ThrowArgumentException(string mail, string role)
        {
            //Arrange
            var service = CreateService();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.GenerateAccessToken(mail, role));
        }

        [Fact]
        public void GenerateAccessToken_Success_ReturnToken()
        {
            //Arrange
            var service = CreateService();
            var mail = "test@mail.com";
            var role = "Test role";
            string secretKey = "FakeSecretKey1111111111111111111";
            string issuer = "FakeIssuer";
            string audience = "FakeAudience";

            _mockAuthenticationSettings.Setup(x => x.Value).Returns(new AuthenticationSetting
            {
                Issuer = issuer,
                Audience = audience,
                SecretKey = secretKey,
                AccessTokenExperiedTime = new AccessTokenExperiedTime { AdminPage = 15, ClientPage = 60 },
                RefreshTokenExperiedTime = new RefreshTokenExperiedTime { AdminPage = 1, ClientPage = 30 }
            });

            //Act
            var result = service.GenerateAccessToken(mail, role);

            //Assert
            Assert.IsType<string>(result);
        }
        #endregion

        #region HandleSocialLogin
        [Theory]
        [InlineData(AuthProvider.Facebook)]
        [InlineData(AuthProvider.Google)]
        public async Task HandleSocialLogin_Failed_InvalidToken_ThrowException(AuthProvider provider)
        {
            //Arrange
            var service = CreateService();
            MockValidator(provider);
            var requestModel = new SocialLoginRequest
            {
                Provider = provider,
                Role = UserRoleEnum.Admin,
                Token = "FakeToken"
            };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => service.HandleSocialLogin(requestModel));
        }


        [Theory]
        [InlineData(AuthProvider.Facebook)]
        [InlineData(AuthProvider.Google)]
        public async Task HandleSocialLogin_Success_WithHasExistUserOnDatabase(AuthProvider provider)
        {
            //Arrange
            var service = CreateService();
            MockValidator(provider);
            var userDatas = CreateUserData();
            var authenAppSetttings = CreateAuthenAppSettingData();
            var userInfo = new ValidateTokenOAuthResponse()
            {
                Email = Mail1,
                Name = "Test"
            };          

            _mockAuthenticationSettings.Setup(x => x.Value).Returns(authenAppSetttings);
            _mockValidatorFactory.Setup(x => x.CreateValidator(provider).ValidateToken(It.IsAny<string>())).ReturnsAsync(userInfo);
            _mockUnitOfWork.Setup(x => x.Repository<Users>()).Returns(userDatas.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<RefreshTokens>()).Returns(CreateRefreshTokenData().MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1).Verifiable();

            var requestModel = new SocialLoginRequest
            {
                Provider = provider,
                Role = UserRoleEnum.Customer,
                Token = "FakeToken"
            };

            // Act & Assert
            var result = await service.HandleSocialLogin(requestModel);

            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(UserRoleEnum.Customer.GetEnumDescription(), result.Role.RoleName);
        }

        [Theory]
        [InlineData(AuthProvider.Facebook)]
        [InlineData(AuthProvider.Google)]
        public async Task HandleSocialLogin_Success_WithNotHasExistUserOnDatabase(AuthProvider provider)
        {
            //Arrange
            var service = CreateService();
            MockValidator(provider);
            var userDatas = CreateUserData();
            var authenAppSetttings = CreateAuthenAppSettingData();
            var userInfo = new ValidateTokenOAuthResponse()
            {
                Email = Mail1,
                Name = "Test1"
            };

            _mockAuthenticationSettings.Setup(x => x.Value).Returns(authenAppSetttings);
            _mockValidatorFactory.Setup(x => x.CreateValidator(provider).ValidateToken(It.IsAny<string>())).ReturnsAsync(userInfo);
            _mockUnitOfWork.Setup(x => x.Repository<Users>()).Returns(userDatas.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<RefreshTokens>()).Returns(CreateRefreshTokenData().MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1).Verifiable(); // Moi lan chay ham saveChange thi se count +1

            var requestModel = new SocialLoginRequest
            {
                Provider = provider,
                Role = UserRoleEnum.Customer,
                Token = "FakeToken"
            };

            // Act & Assert
            var result = await service.HandleSocialLogin(requestModel);

            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(UserRoleEnum.Customer.GetEnumDescription(), result.Role.RoleName);
        }

        #endregion

        #region HandleLoginAsync

        [Fact]
        public async Task HandleLoginAsync_Success()
        {
            //Arrange
            var service = CreateService();
            var userData = CreateUserData();
            //var userRole = CreateUserRoleData();
            var authenAppSetttings = CreateAuthenAppSettingData();
            var requestModel = new UserAuthRequest
            {
                Email = Mail1,
                Password = PassInput1
            };
            _mockAuthenticationSettings.Setup(x => x.Value).Returns(authenAppSetttings);
            _mockUnitOfWork.Setup(x => x.Repository<Users>()).Returns(userData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<RefreshTokens>()).Returns(CreateRefreshTokenData().MockDbSet().Object);
            //Act
            var result = await service.HandleLoginAsync(requestModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
        }

        [Fact]
        public async Task HandleLoginAsync_Failed_InvalidUser_ThrowValidationException()
        {
            //Arrange
            var service = CreateService();
            var userData = CreateUserData();
            

            _mockUnitOfWork.Setup(x => x.Repository<Users>()).Returns(userData.MockDbSet().Object);

            // Act & Assert
            var requestModel = new UserAuthRequest
            {
                Email = Mail2,
                Password = "123456"
            };
            //await Assert.ThrowsAsync<ValidationException>(() => service.HandleLoginAsync(requestModel));
            await Assert.ThrowsAsync<ValidationException>(() => service.HandleLoginAsync(requestModel));
            //Assert.IsType<ValidationException>(exception);
        }
        #endregion

        #region CreateAccount
        [Fact]
        public async Task CreateAccount_Success()
        {
            //Arrange
            var service = CreateService();
            var userData = CreateUserData();
            var authenAppSetttings = CreateAuthenAppSettingData();
            var refreshTokenData = CreateRefreshTokenData();

            _mockAuthenticationSettings.Setup(x => x.Value).Returns(authenAppSetttings);
            _mockUnitOfWork.Setup(x => x.Repository<Users>()).Returns(userData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<RefreshTokens>()).Returns(refreshTokenData.MockDbSet().Object);

            _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1).Verifiable();

            //Set param
            var requestModel = new UserAuthRequest
            {
                Email = "new@mail.com",
                Password = "123456",
                isCustomer = true,
                Role = new RoleModel
                {
                    RoleId = CustomerRoleId,
                    RoleName = UserRoleEnum.Customer.ToString()
                }
            };

            //Act
            var result = await service.CreateAccount(requestModel);

            //Assert
            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
        }

        [Fact]
        public async Task CreateAccount_Failed_ExistAccount_ThrowValidationException()
        {
            //Arrange
            var service = CreateService();
            var userData = CreateUserData();

            _mockUnitOfWork.Setup(x => x.Repository<Users>()).Returns(userData.MockDbSet().Object);

            //Set param
            var requestModel = new UserAuthRequest
            {
                Email = Mail1,
                Password = PassInput1,
                isCustomer = true
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => service.CreateAccount(requestModel));
        }

        #endregion

        #region PrivateFunctions

        private void MockValidator(AuthProvider provider)
        {
            _serviceProvider = UnitTestUtils.CreateServiceProvider();
            switch (provider)
            {
                case AuthProvider.Facebook:
                    var facebookValidator = _serviceProvider.GetRequiredService<FacebookTokenValidator>();
                    _mockValidatorFactory.Setup(x => x.CreateValidator(provider)).Returns(facebookValidator);
                    break;
                case AuthProvider.Google:
                    var googleValidator = _serviceProvider.GetRequiredService<GoogleTokenValidator>();
                    _mockValidatorFactory.Setup(x => x.CreateValidator(provider)).Returns(googleValidator);
                    break;
                default:
                    break;
            }
        }

        private List<Users> CreateUserData()
        {
            return new List<Users>
            {
                new Users
                {
                    Email = Mail1,
                    UserName = "Test",
                    Id = UserId1,
                    PasswordHash = PassHash1,
                    Role = new Roles
                    {
                        Id = Guid.NewGuid(),
                        RoleName = "Customer"
                    }
                }
            };
        }

        private List<RefreshTokens> CreateRefreshTokenData()
        {
            return new List<RefreshTokens>
            {
                new RefreshTokens
                {
                   Id = Guid.NewGuid(),
                   UserId = UserId1,
                   Token = RefreshToken1,
                   Expiry = DateTime.UtcNow.AddDays(3)
                },
                 new RefreshTokens
                {
                   Id = Guid.NewGuid(),
                   UserId = UserId2,
                   Token = RefreshToken2,
                   Expiry = DateTime.UtcNow.AddDays(-1)
                }
            };
        }

        private AuthenticationSetting CreateAuthenAppSettingData()
        {

            return new AuthenticationSetting
            {
                SecretKey = AuthenSecretKey,
                RefreshTokenExperiedTime = new RefreshTokenExperiedTime
                {
                    ClientPage = 30,
                    AdminPage = 1
                },
                AccessTokenExperiedTime = new AccessTokenExperiedTime
                {
                    AdminPage = 15,
                    ClientPage = 60
                }
            };
        }    

        private List<Roles> CreateRoleData()
        {
            return new List<Roles>
            {
                new Roles
                {
                    Id = AdminRoleId,
                    RoleName = "Admin"
                },
                new Roles
                {
                    Id = CustomerRoleId,
                    RoleName = "Customer"
                }
            };
        }

        #endregion
    }
}
