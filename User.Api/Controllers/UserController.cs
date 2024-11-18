using CommonLib.Exceptions;
using CommonLib.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using User.Api.Interfaces;
using User.Api.Models.Requests;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace User.Api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userServices;
        private readonly ILogger<UserController> _logger;
        public UserController(IUser userService, ILogger<UserController> logger)
        {
            _userServices = userService;
            _logger = logger;
        }

        [Route("api/admin/user/search-employee-user")]
        [HttpGet()]
        public async Task<IActionResult> SearchEmployeeUser(int? pageNumber, int? pageSize, string? sortBy,
                                                            string? sortOrder, string? searchInput)
        {   
            try
            {
                _logger.LogInformation($"Start search employee");
                var stopwatch = Stopwatch.StartNew();
               
                var result = await _userServices.SearchEmployeeUserAsync(pageNumber, pageSize, sortBy, sortOrder, searchInput);
                _logger.LogInformation($"Create account successfully.It took {stopwatch.ElapsedMilliseconds} ms to complete.");
                return Ok(ApiResponseHelper.FormatSuccess(new { UserData = result}));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Call SearchEmployeeUser Api was failed.There is a exception!");
                return StatusCode(500, ApiResponseHelper.FormatError($"Call Api SearchEmployeeUser thất bại"));
            }
        }

        [Route("api/admin/user")]
        [HttpGet()]
        public  async Task<IActionResult> GetDataAdminUserPage()
        {
            try
            {
                _logger.LogInformation($"Start get data admin page");
                var stopwatch = Stopwatch.StartNew();
                var result = await _userServices.GetDataAdminUserPageAsync();
                _logger.LogInformation($"Create account successfully.It took {stopwatch.ElapsedMilliseconds} ms to complete.");
                return Ok(ApiResponseHelper.FormatSuccess(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Call GeDataAdminUserPage Api was failed.There is a exception!");
                return StatusCode(500, ApiResponseHelper.FormatError($"Call Api GeDataAdminUserPage thất bại"));
            }
        }

        [Route("api/[controller]/register")]
        [HttpPost()]
        public async Task<IActionResult> RegisterUser([FromBody] UserAuthRequest request)
        {
            try
            {
                _logger.LogInformation($"Start register user");
                var stopwatch = Stopwatch.StartNew();
                var result = await _userServices.CreateAccountAsync(request);
                if (result == null)
                {
                    return BadRequest(ApiResponseHelper.FormatError($"Quá trình tạo tài khoản thất bại"));
                }

                _logger.LogInformation($"Create account successfully.It took {stopwatch.ElapsedMilliseconds} ms to complete.");
                return Ok(ApiResponseHelper.FormatSuccess(result));
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ApiResponseHelper.FormatError($"{ex.Message}"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create account failed.There is a exception!");
                return StatusCode(500, ApiResponseHelper.FormatError($"Quá trình tạo tài khoản thất bại"));
            }
        }

        [Route("api/[controller]/update")]
        [HttpPost()]
        public async Task<IActionResult> UpdateUser([FromBody] UserAuthRequest request)
        {
            try
            {
                _logger.LogInformation($"Start update user. UserId: {request.Id}");
                var stopwatch = Stopwatch.StartNew();
                var result = await _userServices.UpdateUserAsync(request);

                _logger.LogInformation($"Update user {request.Id} successfully.It took {stopwatch.ElapsedMilliseconds} ms to complete.");
                return Ok(ApiResponseHelper.FormatSuccess(result));
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ApiResponseHelper.FormatError($"{ex.Message}"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Update user {request.Id} failed. Message: {e.Message}");
                return StatusCode(500, ApiResponseHelper.FormatError($"Update user api failed"));
            }
        }

        [Route("api/admin/[controller]/delete")]
        [HttpPost()]
        public async Task<IActionResult> DeleteUser([FromBody]DeleteUserRequest request)
        {
            try
            {
                _logger.LogInformation("Start delete user.");
                var stopwatch = Stopwatch.StartNew();
                var result = await _userServices.DeleteUserAsync(request);
                _logger.LogInformation($"Delete user {request.UserId} successfully.It took {stopwatch.ElapsedMilliseconds} ms to complete.");
                return Ok(ApiResponseHelper.FormatSuccess(result));
            }
            catch (BusinessException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ApiResponseHelper.FormatError($"{ex.Message}"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Delete user {request.UserId} failed.There is a exception!");
                return StatusCode(500, ApiResponseHelper.FormatError($"Delete user api failed"));
            }
        }

        [Route("api/[controller]/login")]
        [HttpPost()]
        public async Task<IActionResult> Login([FromBody] UserAuthRequest request)
        {
            try
            {
                var stopwatch = Stopwatch.StartNew();
                var result = await _userServices.HandleLoginAsync(request);

                _logger.LogInformation("Login successfully !");
                _logger.LogInformation($"Login operation took {stopwatch.ElapsedMilliseconds} ms to complete.");             
                return Ok(ApiResponseHelper.FormatSuccess(result));
            }
            catch (ValidationException e)
            {
                _logger.LogError(e, "Login failed");
                return Unauthorized(ApiResponseHelper.FormatError(e.Message));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Login failed");
                return StatusCode(500, ApiResponseHelper.FormatError($"Quá trình đăng nhập thất bại."));
            }
        }

        [Route("api/[controller]/social-login")]
        [HttpPost()]
        public async Task<IActionResult> SocialLogin([FromBody] SocialLoginRequest request)
        {
            try
            {
                var stopwatch = Stopwatch.StartNew();
                var result = await _userServices.HandleSocialLoginAsync(request);

                _logger.LogInformation("Login successfully !");
                _logger.LogInformation($"Login operation took {stopwatch.ElapsedMilliseconds} ms to complete.");
                return Ok(ApiResponseHelper.FormatSuccess(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Social login failed.There is a exception !");
                return StatusCode(500, ApiResponseHelper.FormatError($"Quá trình đăng nhập bằng tài khoản {request.Provider} thất bại"));
            }
        }

        [Route("api/[controller]/refresh-token")]
        [HttpPost()]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestModel model)
        {
            try
            {
                var result = await _userServices.RefreshTokenAsync(model);
                if (string.IsNullOrEmpty(result))
                {
                    return Unauthorized("Invalid or expired refresh token");
                }
                _logger.LogInformation("Refresh token successfully !");
                return Ok(ApiResponseHelper.FormatSuccess(new { accessToken = result }));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Refresh token failed.There is a exception !");
                return StatusCode(500, ApiResponseHelper.FormatError($"Renew refresh token failed."));
            }
        }
    }
}
