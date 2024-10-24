using Comman.Domain.Models;
using CommonLib.Constants;
using CommonLib.Helpers;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json.Serialization;
using User.Api.Implements;
using User.Api.Interfaces;
using User.Api.Models.Requests;
using Newtonsoft.Json;
using System;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserAuthRequest request)
        {
            try
            {
                var stopwatch = Stopwatch.StartNew();
                var result = await _userServices.CreateAccount(request);
                if (result == null)
                {
                    return BadRequest(ApiResponseHelper.FormatError($"Quá trình tạo tài khoản thất bại"));
                }

                _logger.LogInformation($"Create account successfully.It took {stopwatch.ElapsedMilliseconds} ms to complete.");
                return Ok(ApiResponseHelper.FormatSuccess(result));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ApiResponseHelper.FormatError($"Quá trình tạo tài khoản thất bại. {ex.Message}"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create account failed.There is a exception!");
                return StatusCode(500, ApiResponseHelper.FormatError($"Quá trình tạo tài khoản thất bại"));
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserAuthRequest request)
        {
            _logger.LogInformation("Start Api Login");
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

        [HttpPost("social-login")]
        public async Task<IActionResult> SocialLogin([FromBody] SocialLoginRequest request)
        {
            try
            {
                var stopwatch = Stopwatch.StartNew();
                var result = await _userServices.HandleSocialLogin(request);

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

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestModel model)
        {
            try
            {
                var result = await _userServices.RefreshToken(model);
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
