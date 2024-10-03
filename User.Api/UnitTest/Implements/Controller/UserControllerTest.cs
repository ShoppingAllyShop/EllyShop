using Comman.Domain.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel.DataAnnotations;
using User.Api.Controllers;
using User.Api.Implements;
using User.Api.Interfaces;
using User.Api.Models.Requests;
using User.Api.Models.Responses;
using Xunit;
using static CommonLib.Constants.AppEnums;

namespace User.Api.UnitTest.Implements.Controller
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
            _mockUserServices.Setup(x => x.CreateAccount(It.IsAny<UserAuthRequest>())).ReturnsAsync(mockResult);


            //Act
            var requestModel = new UserAuthRequest
            {
                Email = Mail1,
                Password = PassInput1
            };
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
            _mockUserServices.Setup(x => x.CreateAccount(It.IsAny<UserAuthRequest>())).ReturnsAsync(mockResult);


            //Act
            var requestModel = new UserAuthRequest
            {
                Email = Mail1,
                Password = PassInput1
            };
            var result = await _userController.RegisterUser(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task RegisterUser_Failed_ThrowValidationException_ReturnBadRequest_400()
        {
            //Arrange
            _mockUserServices.Setup(x => x.CreateAccount(It.IsAny<UserAuthRequest>())).Throws(new ValidationException());

            //Act
            var requestModel = new UserAuthRequest
            {
                Email = Mail2,
                Password = PassInput1
            };
            var result = await _userController.RegisterUser(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task RegisterUser_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange
            _mockUserServices.Setup(x => x.CreateAccount(It.IsAny<UserAuthRequest>())).Throws(new Exception());

            //Act
            var requestModel = new UserAuthRequest
            {
                Email = Mail1,
                Password = PassInput1
            };
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
            var requestModel = new UserAuthRequest
            {
                Email = Mail1,
                Password = PassInput1
            };
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
            var requestModel = new UserAuthRequest
            {
                Email = Mail2,
                Password = PassInput1
            };
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
            var requestModel = new UserAuthRequest
            {
                Email = Mail1,
                Password = PassInput1
            };
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
            _mockUserServices.Setup(x => x.HandleSocialLogin(It.IsAny<SocialLoginRequest>())).ReturnsAsync(mockResult);


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
            _mockUserServices.Setup(x => x.HandleSocialLogin(It.IsAny<SocialLoginRequest>())).Throws(new Exception());

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
            _mockUserServices.Setup(x => x.RefreshToken(It.IsAny<RefreshTokenRequestModel>())).ReturnsAsync("token");

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
            _mockUserServices.Setup(x => x.RefreshToken(It.IsAny<RefreshTokenRequestModel>())).ReturnsAsync(string.Empty);

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
            _mockUserServices.Setup(x => x.RefreshToken(It.IsAny<RefreshTokenRequestModel>())).Throws(new Exception());

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
        #endregion
    }
}
