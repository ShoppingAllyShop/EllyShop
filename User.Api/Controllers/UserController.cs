using Comman.Domain.Models;
using CommonLib.Constants;
using CommonLib.Helpers;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using User.Api.Implements;
using User.Api.Interfaces;
using User.Api.Models.Requests;

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
        // GET: api/<UserController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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
                _logger.LogError(e, "Create account failed.There is a exception !");
                return BadRequest(ApiResponseHelper.FormatError($"Quá trình tạo tài khoản thất bại"));
            }
        }


        [HttpPost("login")]
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
                return Ok(ApiResponseHelper.FormatError(e.Message));               
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Login failed");
                return BadRequest(ApiResponseHelper.FormatError($"Quá trình đăng nhập thất bại. {e.Message}"));
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
                _logger.LogError(e,"Social login failed.There is a exception !");
                return BadRequest(ApiResponseHelper.FormatError($"Quá trình đăng nhập bằng tài khoản {request.Provider} thất bại"));
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
                return Ok(ApiResponseHelper.FormatSuccess(new {accessToken= result}));
            }
            catch (Exception e)
            {
                return BadRequest(ApiResponseHelper.FormatError($"Renew refresh token failed. {e.Message}"));
            }
        }
    }
}
