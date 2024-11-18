using Comman.Domain.Elly_User;
using CommonLib.Constants;
using CommonLib.Exceptions;
using CommonLib.Models.Base;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel.DataAnnotations;
using User.Api.Controllers;
using User.Api.Interfaces;
using User.Api.Models.Requests;
using User.Api.Models.Responses;
using User.Api.UnitTest.InitData;
using Xunit;
using static CommonLib.Constants.AppEnums;

namespace User.Api.UnitTest.Controller
{
    public class UserControllerTest
    {
        private UserController _userController;
        private Mock<IUser> _mockUserServices = new Mock<IUser>();
        private Mock<ILogger<UserController>> _mockLogger = new Mock<ILogger<UserController>>();

        private readonly string Mail1 = "Test1@gmail.com";
        private readonly string Mail2 = "Test2@gmail.com";
        private readonly string PassInput1 = "123456";
        private readonly string PassHash1 = "AvN8e2kj9PwpW/Qo56eCkA==:0Fz0jQa0hloGaqWQ0ydE0hPM6LY=";
        private readonly Guid UserId1 = new Guid("af24e5b0-a03d-4301-b54d-2229867604a9");

        public UserControllerTest()
        {
            _userController = new UserController(
              _mockUserServices.Object,
              _mockLogger.Object);
        }

        #region DeleteUser
        [Fact]
        public async Task DeleteUser_Success_ReturnOk_200()
        {
            //Arrange
            var mockResult = CreateDeleteUserResponse();
            var requestModel = CreateDeleteUserRequest();

            _mockUserServices.Setup(x => x.DeleteUserAsync(It.IsAny<DeleteUserRequest>())).ReturnsAsync(mockResult);

            //Act
            var result = await _userController.DeleteUser(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task DeleteUser_Failed_ThrowBusinessException_ReturnBadRequest_400()
        {
            //Arrange
            _mockUserServices.Setup(x => x.DeleteUserAsync(It.IsAny<DeleteUserRequest>())).Throws(new BusinessException());
            var requestModel = CreateDeleteUserRequest();

            //Act
            var result = await _userController.DeleteUser(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task DeleteUser_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange
            _mockUserServices.Setup(x => x.DeleteUserAsync(It.IsAny<DeleteUserRequest>())).Throws(new Exception());

            //Act
            var requestModel = CreateDeleteUserRequest();
            var result = await _userController.DeleteUser(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        #endregion

        #region UpdateUser
        [Fact]
        public async Task UpdateUser_Success_ReturnOk_200()
        {
            //Arrange
            var mockResult = "Name";
            var requestModel = MockData.CreateUserAuthRequest();

            _mockUserServices.Setup(x => x.UpdateUserAsync(It.IsAny<UserAuthRequest>())).ReturnsAsync(mockResult);

            //Act
            var result = await _userController.UpdateUser(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task UpdateUser_Failed_ThrowValidationException_ReturnBadRequest_400()
        {
            //Arrange
            _mockUserServices.Setup(x => x.UpdateUserAsync(It.IsAny<UserAuthRequest>())).Throws(new ValidationException());
            var requestModel = MockData.CreateUserAuthRequest();

            //Act
            var result = await _userController.UpdateUser(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task UpdateUser_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange
            _mockUserServices.Setup(x => x.UpdateUserAsync(It.IsAny<UserAuthRequest>())).Throws(new Exception());

            //Act
            var requestModel = MockData.CreateUserAuthRequest();
            var result = await _userController.UpdateUser(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        #endregion

        #region SearchEmployeeUser

        [Fact]
        public async Task SearchEmployeeUser_Success_ReturnOk_200()
        {
            //Arrange
            SearchEmployeeUserResponse mockResult = CreateSearchEmployeeUserResponse();

            _mockUserServices.Setup(x => x.SearchEmployeeUserAsync(It.IsAny<int>(), It.IsAny<int>()
                    , It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(mockResult);


            //Act
            var result = await _userController.SearchEmployeeUser(null, null, null, null, null);

            //Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task SearchEmployeeUser_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange
            _mockUserServices.Setup(x => x.SearchEmployeeUserAsync(It.IsAny<int>(), It.IsAny<int>()
                   , It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception());
            //Act
            var result = await _userController.SearchEmployeeUser(1, 10, null, null, null);

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        #endregion

        #region GetDataAdminUserPage
        [Fact]
        public async Task GetDataAdminUserPage_Success_ReturnOk_200()
        {
            //Arrange
            DataAdminUserPageResponse mockResult = new DataAdminUserPageResponse
            {
                ContentPageData = new ContentPageData
                {
                    Departments = MockData.CreateDepartmentList(),
                    Roles = MockData.CreateRoleList(),
                    Positions = MockData.CreatePositionList()
                },
                UserData = new SearchEmployeeUserResponse
                {
                    Paging = new PagingResponseBase
                    {
                        PageNumber = 1,
                        PageSize = 10,
                        SortBy = "UserName",
                        SortOrder = "asc",
                        TotalItems = 30
                    },
                    UserList = CreateUserInfoList()
                }
            };

            _mockUserServices.Setup(x => x.GetDataAdminUserPageAsync()).ReturnsAsync(mockResult);


            //Act
            var result = await _userController.GetDataAdminUserPage();

            //Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task GetDataAdminUserPage_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange
            _mockUserServices.Setup(x => x.GetDataAdminUserPageAsync()).Throws(new Exception());
            //Act
            var result = await _userController.GetDataAdminUserPage();

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        #endregion

        #region RegisterUser

        [Fact]
        public async Task RegisterUser_Success_ReturnOk_200()
        {
            //Arrange
            UserResponse mockResult = new UserResponse
            {
                Email = Mail1,
                AccessToken = "FakeToken",
                RefreshToken = "FakeToken",
                UserName = "Test"
            };
            _mockUserServices.Setup(x => x.CreateAccountAsync(It.IsAny<UserAuthRequest>())).ReturnsAsync(mockResult);


            //Act
            var requestModel = MockData.CreateUserAuthRequest();
            var result = await _userController.RegisterUser(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task RegisterUser_Failed_WithNullResult_ReturnBadRequest_400()
        {
            //Arrange
            UserResponse? mockResult = null;
            _mockUserServices.Setup(x => x.CreateAccountAsync(It.IsAny<UserAuthRequest>())).ReturnsAsync(mockResult);


            //Act
            var requestModel = MockData.CreateUserAuthRequest();
            var result = await _userController.RegisterUser(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task RegisterUser_Failed_ThrowValidationException_ReturnBadRequest_400()
        {
            //Arrange
            _mockUserServices.Setup(x => x.CreateAccountAsync(It.IsAny<UserAuthRequest>())).Throws(new ValidationException());

            //Act
            var requestModel = MockData.CreateUserAuthRequest();
            var result = await _userController.RegisterUser(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task RegisterUser_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange
            _mockUserServices.Setup(x => x.CreateAccountAsync(It.IsAny<UserAuthRequest>())).Throws(new Exception());

            //Act
            var requestModel = MockData.CreateUserAuthRequest();
            var result = await _userController.RegisterUser(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        #endregion

        #region Login

        [Fact]
        public async Task Login_Success_ReturnOk_200()
        {
            //Arrange
            UserResponse mockResult = new UserResponse
            {
                Email = Mail1,
                AccessToken = "FakeToken",
                RefreshToken = "FakeToken",
                UserName = "Test"
            };
            _mockUserServices.Setup(x => x.HandleLoginAsync(It.IsAny<UserAuthRequest>())).ReturnsAsync(mockResult);


            //Act
            var requestModel = MockData.CreateUserAuthRequest();
            var result = await _userController.Login(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Login_Failed_ThrowValidationException_ReturnUnauthorized_401()
        {
            //Arrange
            _mockUserServices.Setup(x => x.HandleLoginAsync(It.IsAny<UserAuthRequest>())).Throws(new ValidationException());

            //Act
            var requestModel = MockData.CreateUserAuthRequest();
            var result = await _userController.Login(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal(401, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task Login_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange
            _mockUserServices.Setup(x => x.HandleLoginAsync(It.IsAny<UserAuthRequest>())).Throws(new Exception());

            //Act
            var requestModel = MockData.CreateUserAuthRequest();
            var result = await _userController.Login(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        #endregion

        #region SocialLogin
        [Fact]
        public async Task SocialLogin_Success_ReturnOk_200()
        {
            //Arrange
            UserResponse mockResult = new UserResponse
            {
                Email = Mail1,
                AccessToken = "FakeToken",
                RefreshToken = "FakeToken",
                UserName = "Test"
            };
            _mockUserServices.Setup(x => x.HandleSocialLoginAsync(It.IsAny<SocialLoginRequest>())).ReturnsAsync(mockResult);


            //Act
            var requestModel = new SocialLoginRequest
            {
                Provider = AuthProvider.Facebook,
                Token = "FakeToken"
            };
            var result = await _userController.SocialLogin(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task SocialLogin_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange
            _mockUserServices.Setup(x => x.HandleSocialLoginAsync(It.IsAny<SocialLoginRequest>())).Throws(new Exception());

            //Act
            var requestModel = new SocialLoginRequest
            {
                Provider = AuthProvider.Facebook,
                Token = "FakeToken"
            };
            var result = await _userController.SocialLogin(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        #endregion

        #region RefreshToken
        [Fact]
        public async Task RefreshToken_Success_ReturnOk_200()
        {
            //Arrange
            _mockUserServices.Setup(x => x.RefreshTokenAsync(It.IsAny<RefreshTokenRequestModel>())).ReturnsAsync("token");

            //Act
            var result = await _userController.RefreshToken(CreateRefreshTokenRequestModel());

            //Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task RefreshToken_Failed_InvalidToken_ReturnUnauthorized_401()
        {
            //Arrange
            _mockUserServices.Setup(x => x.RefreshTokenAsync(It.IsAny<RefreshTokenRequestModel>())).ReturnsAsync(string.Empty);

            //Act
            var result = await _userController.RefreshToken(CreateRefreshTokenRequestModel());

            //Assert
            var statusCodeResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal(401, statusCodeResult.StatusCode);
        }


        [Fact]
        public async Task RefreshToken_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange
            _mockUserServices.Setup(x => x.RefreshTokenAsync(It.IsAny<RefreshTokenRequestModel>())).Throws(new Exception());

            //Act            
            var result = await _userController.RefreshToken(CreateRefreshTokenRequestModel());

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        #endregion

        #region Private
        private List<Users> CreateUserData()
        {
            return new List<Users>
            {
                new Users
                {
                    Email = Mail1,
                    UserName = "Test",
                    Id = UserId1,
                    PasswordHash = PassHash1
                }
            };
        }

        private RefreshTokenRequestModel CreateRefreshTokenRequestModel()
        {
            return new RefreshTokenRequestModel
            {
                Email = Mail1,
                RefreshToken = "FakeRefreshToken",
                UserName = "Test"
            };
        }

        private List<UserInfo> CreateUserInfoList()
        {
            return new List<UserInfo>
               {
                   new UserInfo
                   {
                       UserId = Guid.NewGuid(),
                       Email = "Email1",
                       RoleId = new Guid(GuidContstants.AdminRole),
                       UserName = "Name1"
                   },
                   new UserInfo
                   {
                       UserId = Guid.NewGuid(),
                       Email = "Email2",
                       RoleId = new Guid(GuidContstants.AdminRole),
                       UserName = "Name2"
                   },
               };
        }        

        private DeleteUserRequest CreateDeleteUserRequest()
        {
            return new DeleteUserRequest
            {
                PageNumber = 1,
                PageSize = 10,
                SortBy = "UserName",
                SortOrder = "asc",
                SearchInput = "",
                UserId = UserId1
            };
        }

        private DeleteUserResponse CreateDeleteUserResponse()
        {
            return new DeleteUserResponse
            {
                PagingUserList = CreateSearchEmployeeUserResponse(),
                UserName = "Name"
            };
        }

        private SearchEmployeeUserResponse CreateSearchEmployeeUserResponse()
        {
            return new SearchEmployeeUserResponse
            {
                Paging = new PagingResponseBase
                {
                    PageNumber = 1,
                    PageSize = 10,
                    SortBy = "UserName",
                    SortOrder = "asc",
                    TotalItems = 30
                },
                UserList = CreateUserInfoList()
            };
        }
        #endregion
    }
}
