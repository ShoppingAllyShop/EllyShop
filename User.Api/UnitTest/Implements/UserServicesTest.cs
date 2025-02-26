using Comman.Domain.Elly_User;
using Common.Infrastructure.Interfaces;
using CommonLib.Constants;
using CommonLib.Enums;
using CommonLib.Exceptions;
using CommonLib.Helpers.Interfaces;
using CommonLib.Models.Settings;
using CommonLib.TestHelpers;
using Microsoft.Extensions.Options;
using Moq;
using System.ComponentModel.DataAnnotations;
using User.Api.Implements;
using User.Api.Implements.TokenValidator;
using User.Api.Interfaces.Factory;
using User.Api.Models;
using User.Api.Models.Requests;
using User.Api.Models.Responses;
using User.Api.UnitTest.InitData;
using Xunit;
using static CommonLib.Constants.AppEnums;

namespace User.Api.UnitTest.Implements
{
    public class UserServicesTest
    {
        private Mock<IOptions<AuthenticationSetting>> _mockAuthenticationSettings = new Mock<IOptions<AuthenticationSetting>>();
        private Mock<IOptions<FacebookSetting>> _mockFacebookSettings = new Mock<IOptions<FacebookSetting>>();
        private Mock<ILogger<UserServices>> _mockLogger = new Mock<ILogger<UserServices>>();
        private Mock<IUnitOfWork<Elly_UserContext>> _mockUnitOfWork = new Mock<IUnitOfWork<Elly_UserContext>>();
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
        private readonly string RefreshToken3 = "JSNtfseRpkkj3m59bVe1cJN6666/mTLRTVIwLl5w4c=";
        private readonly Guid UserId1 = new Guid("af24e5b0-a03d-4301-b54d-2229867604a9");
        private readonly Guid UserId2 = new Guid("af24e5b0-a03d-4301-b54d-2229867604a8");
        private readonly Guid UserId3 = new Guid("af24e5b0-a03d-4301-b54d-2229867604a7");

        private readonly Guid AdminRoleId = new Guid("5A387B6E-1B29-47E1-84F6-D25D4287B11C");
        private readonly Guid CustomerRoleId = new Guid("87A989E6-C641-4222-AF8B-9CFC3C9B1183");
        private readonly string AuthenSecretKey = "FakeSecretKey1111111111111111111";

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
                RoleId = Guid.NewGuid(),
                RoleName = UserRoleEnum.Customer.GetEnumDescription()
            };

            _mockAuthenticationSettings.Setup(x => x.Value).Returns(authenAppSetttings);
            _mockUnitOfWork.Setup(x => x.Repository<RefreshTokens>()).Returns(CreateRefreshTokenData().MockDbSet().Object);

            //Act
            var result = await service.RefreshTokenAsync(requestModel);

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
            var result = await service.RefreshTokenAsync(requestModel);

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
            var result = await service.RefreshTokenAsync(requestModel);

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
            await Assert.ThrowsAsync<Exception>(() => service.HandleSocialLoginAsync(requestModel));
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
                Email = "mail3",
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
            var result = await service.HandleSocialLoginAsync(requestModel);

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
                Email = "mail3",
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
            var result = await service.HandleSocialLoginAsync(requestModel);

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
                Password = "1234567"
            };

            await Assert.ThrowsAsync<ValidationException>(() => service.HandleLoginAsync(requestModel));
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
                RoleId = CustomerRoleId,
                RoleName = UserRoleEnum.Customer.ToString()
            };

            //Act
            var result = await service.CreateAccountAsync(requestModel);

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
            await Assert.ThrowsAsync<ValidationException>(() => service.CreateAccountAsync(requestModel));
        }

        #endregion

        #region SearchEmployeeUserAsync
        [Fact]
        public async Task SearchEmployeeUserAsync_Success()
        {
            //Arrange
            var service = CreateService();
            var userList = CreateUserData();
            _mockUnitOfWork.Setup(x => x.Repository<Users>()).Returns(userList.MockDbSet().Object);

            //Act
            var result = await service.SearchEmployeeUserAsync(1, 10, null, null, null);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<SearchEmployeeUserResponse>(result);
        }
        #endregion

        #region GetDataAdminUserPage
        [Fact]
        public async Task GetDataAdminUserPage_Success()
        {
            //Arrange
            var service = CreateService();
            var userList = CreateUserData();
            var positionList = MockData.CreatePositionList();
            var departmentList = MockData.CreateDepartmentList();
            var roleList = MockData.CreateRoleList();

            _mockUnitOfWork.Setup(x => x.Repository<Position>()).Returns(positionList.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<Department>()).Returns(departmentList.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<Roles>()).Returns(roleList.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<Users>()).Returns(userList.MockDbSet().Object);

            //Act
            var result = await service.GetDataAdminUserPageAsync();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<DataAdminUserPageResponse>(result);
        }
        #endregion

        #region UpdateUserAsync
        [Fact]
        public async Task UpdateUserAsync_Success()
        {
            //Arranges
            var service = CreateService();
            var userList = CreateUserData();
            var requestModel = new UserAuthRequest
            {
                Id = UserId1,
                UserName = "New name",
                RoleId = new Guid(GuidContstants.CustomerRole)
            };
            var positionList = MockData.CreatePositionList();
            var departmentList = MockData.CreateDepartmentList();
            var roleList = MockData.CreateRoleList();

            _mockUnitOfWork.Setup(x => x.Repository<Users>()).Returns(userList.MockDbSet().Object);

            //Act
            var result = await service.UpdateUserAsync(requestModel);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task UpdateUserAsync_Failed_InvalidUser_ThrowValidationException()
        {
            //Arrange
            var service = CreateService();
            var userData = CreateUserData();
            var requestModel = MockData.CreateUserAuthRequest();

            _mockUnitOfWork.Setup(x => x.Repository<Users>()).Returns(userData.MockDbSet().Object);

            // Act & Assert            
            await Assert.ThrowsAsync<ValidationException>(() => service.UpdateUserAsync(requestModel));
        }
        #endregion

        #region UpdateUserAsync
        [Fact]
        public async Task DeleteUserAsync_Success()
        {
            //Arranges
            var service = CreateService();
            var userList = CreateUserData();
            var requestModel = new DeleteUserRequest
            {
                UserId = UserId1,
                PageNumber = 1,
                PageSize = 10,
                SearchInput = ""
            };
            var mockRefreshToken = new List<RefreshTokens>
            {
                new RefreshTokens
                {
                    Id = Guid.NewGuid(),
                    Token = "token",
                    Expiry = DateTime.UtcNow.AddDays(3),
                    UserId = UserId1
                }
            };
            var positionList = MockData.CreatePositionList();
            var departmentList = MockData.CreateDepartmentList();
            var roleList = MockData.CreateRoleList();

            _mockUnitOfWork.Setup(x => x.Repository<Users>()).Returns(userList.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<RefreshTokens>()).Returns(mockRefreshToken.MockDbSet().Object);

            //Act
            var result = await service.DeleteUserAsync(requestModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<DeleteUserResponse>(result);
        }

        [Fact]
        public async Task DeleteUserAsync_Failed_InvalidUser_ThrowBusinessException()
        {
            //Arrange
            var service = CreateService();
            var userData = CreateUserData();
            var requestModel = new DeleteUserRequest
            {
                UserId = Guid.NewGuid(),
                PageNumber = 1,
                PageSize = 10,
                SearchInput = ""
            };

            _mockUnitOfWork.Setup(x => x.Repository<Users>()).Returns(userData.MockDbSet().Object);

            // Act & Assert            
            await Assert.ThrowsAsync<BusinessException>(() => service.DeleteUserAsync(requestModel));
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
                    RoleId = new Guid(GuidContstants.AdminRole),
                    Role = new Roles
                    {
                        Id = new Guid(GuidContstants.AdminRole),
                        RoleName = "Admin"
                    }
                },
                new Users
                {
                    Id = Guid.NewGuid(),
                    Email = Mail2,
                    UserName = "name2",
                    PasswordHash = PassHash1,
                    RoleId = new Guid(GuidContstants.AdminRole),
                    Role = new Roles
                    {
                        Id = new Guid(GuidContstants.AdminRole),
                        RoleName = "Admin"
                    }
                },
                new Users
                {
                    Id = UserId3,
                    Email = "mail3",
                    UserName = "name3",
                    RoleId = new Guid(GuidContstants.CustomerRole),
                    Role = new Roles
                    {
                        Id = new Guid(GuidContstants.CustomerRole),
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
                },
                 new RefreshTokens
                 {
                     Id = Guid.NewGuid(),
                     UserId = UserId3,
                     Token = RefreshToken3,
                     Expiry = DateTime.UtcNow.AddDays(3)
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
        #endregion
    }
}
