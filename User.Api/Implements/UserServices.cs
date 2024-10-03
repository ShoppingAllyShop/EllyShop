using System.Security.Claims;
using System.Text;
using User.Api.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using CommonLib.Models.Settings;
using Google.Apis.Auth;
using Newtonsoft.Json.Linq;
using User.Api.Models;
using System.Security.Cryptography;
using Comman.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Common.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using CommonLib.Constants;
using Microsoft.VisualBasic;
using static CommonLib.Constants.AppEnums;
using User.Api.Constant;
using System.Runtime.CompilerServices;
using CommonLib.Helpers.Interfaces;
using System.ComponentModel.DataAnnotations;
using User.Api.Interfaces.Factory;
using CommonLib.Enums;
using User.Api.Models.Requests;
using User.Api.Models.Responses;
using Google.Apis.Auth.OAuth2.Requests;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text.Json;
using Moq;

namespace User.Api.Implements
{
    public class UserServices : IUser
    {
        private readonly IOptions<AuthenticationSetting> _authenticationSettings;
        private readonly IOptions<FacebookSetting> _facebookSettings;
        private readonly ILogger<UserServices> _logger;
        private readonly IUnitOfWork<EllyShopContext> _unitOfWork;
        private readonly IApiServices _apiServices;
        private readonly ITokenValidatorFactory _validatorFactory;

        public UserServices(IOptions<AuthenticationSetting> authenticationSettings,
            IOptions<FacebookSetting> facebookSettings,
            ILogger<UserServices> logger,
            IUnitOfWork<EllyShopContext> unitOfWork, IApiServices apiServices, ITokenValidatorFactory validatorFactory)
        {
            _authenticationSettings = authenticationSettings;
            _facebookSettings = facebookSettings;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _apiServices = apiServices;
            _validatorFactory = validatorFactory;
        }

        public string GenerateAccessToken(string email, string roleName)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException($"Generate access token failed. Parameters can be not empty");
            }

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_authenticationSettings.Value.SecretKey);
            var secretKey = new SymmetricSecurityKey(secretKeyBytes);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim("Role", roleName)
            };
            var expiredTime = roleName != UserRoleEnum.Customer.ToString() ?
                                DateTime.UtcNow.AddMinutes(_authenticationSettings.Value.AccessTokenExperiedTime.ClientPage) :
                                DateTime.UtcNow.AddMinutes(_authenticationSettings.Value.AccessTokenExperiedTime.AdminPage);
            JwtSecurityToken tokenOptions = new JwtSecurityToken(
                issuer: _authenticationSettings.Value.Issuer,
                claims: claims,
                audience: _authenticationSettings.Value.Audience,
                expires: expiredTime,
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature)
                );

            string accessToken = jwtTokenHandler.WriteToken(tokenOptions);
            _logger.LogInformation($"Function {nameof(GenerateAccessToken)} was done.");
            return accessToken;
        }

        public async Task<UserResponse> HandleSocialLogin(SocialLoginRequest request)
        {
            var validator = _validatorFactory.CreateValidator(request.Provider);
            var resultValidateToken = await validator.ValidateToken(request.Token);
            if (resultValidateToken == null) throw new Exception("validating the token of social login failed. Return null result");

            // Kiểm tra người dùng trong DB hoặc tạo mới nếu cần
            var newRefreshToken = string.Empty;
            var isExistUser = await _unitOfWork.Repository<Users>().AnyAsync(x => x.Email == resultValidateToken.Email);            
            if (!isExistUser)
            {
                var createdTime = DateTime.UtcNow;
                newRefreshToken = GenerateRefreshToken();
                //create user
                var newUserId = Guid.NewGuid();
                var newUser = new Users()
                {
                    Id = newUserId,
                    Email = resultValidateToken.Email,
                    CreatedTime = createdTime,
                    UserName = resultValidateToken.Name,
                    RoleId = new Guid(GuidContstants.CustomerRole) //Chỉ có Customer mới login bằng social
                };
                await _unitOfWork.Repository<Users>().AddAsync(newUser);
                var newRefreshTokenObj = new RefreshTokens()
                {
                    Id = Guid.NewGuid(),
                    UserId = newUserId,
                    Token = newRefreshToken,
                    Expiry = DateTime.UtcNow.AddDays(3),
                    CreatedAt = createdTime,
                    UpdatedAt = createdTime
                };              

                await _unitOfWork.Repository<RefreshTokens>().AddAsync(newRefreshTokenObj);
                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("Create new user and stored database successfully");
            }
            else
            {
                newRefreshToken = await RenewRefreshToken(resultValidateToken.Email, true);
            }

            //Tạo access token và result
            //var isCustomer = true; //Chỉ client page mới xài social login
            var user = await _unitOfWork.Repository<Users>().Include(x => x.Role).FirstOrDefaultAsync(x => x.Email == resultValidateToken.Email);
            var accessToken = GenerateAccessToken(resultValidateToken.Email, user?.Role.RoleName ?? string.Empty);
           
            var result = new UserResponse()
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken,
                UserName = user?.UserName,
                Email = user?.Email,
                Role = new RoleModel
                {
                    RoleId = user.Role.Id,
                    RoleName = user.Role.RoleName
                }
            };

            _logger.LogInformation($"Function {nameof(HandleSocialLogin)} was done.");
            return result;
        }      

        public async Task<string> RefreshToken(RefreshTokenRequestModel requestModel)
        {
            //validate refresh token
            var storedToken = await GetRefreshToken(requestModel.RefreshToken);
            if (storedToken == null) return string.Empty;

            var isValidRefreshToken = ValidateRefreshToken(storedToken, requestModel.RefreshToken);
            if (!isValidRefreshToken) return string.Empty;

            //create new access token
            var newAccessToken = GenerateAccessToken(requestModel.Email, requestModel.Role.RoleName);
            return newAccessToken;
        }

        public async Task<UserResponse> HandleLoginAsync(UserAuthRequest model)
        {
            var validatedUserResult = await ValidateUserAsync(model.Email, model.Password);
            if (validatedUserResult == null)
            {
                throw new ValidationException("Email hoặc mật khẩu không đúng");
            }

            var renewRefreshToken = await RenewRefreshToken(model.Email, model.isCustomer);
            var accessToken = GenerateAccessToken(model.Email, validatedUserResult.Role.RoleName);
            
            return new UserResponse()
            {
                AccessToken = accessToken,
                Email = model.Email,
                RefreshToken = renewRefreshToken,
                UserName = validatedUserResult.UserName,
                Role = new RoleModel
                {
                    RoleId = validatedUserResult.Role.Id,
                    RoleName = validatedUserResult.Role.RoleName
                }
            };
        }
        public async Task<UserResponse?> CreateAccount(UserAuthRequest model)
        {
            var isHasAccount = await _unitOfWork.Repository<Users>().AsNoTracking().AnyAsync(x => x.Email == model.Email);
            if (isHasAccount) throw new ValidationException("Email này đã tồn tại");

            //storing database
            string hashedPassword = HashPasswordWithSalt(model.Password);
            var refreshToken = GenerateRefreshToken();
            var createdTime = DateTime.UtcNow;

            //create user
            var userId = Guid.NewGuid();
            var roleId = model.isCustomer ? new Guid (GuidContstants.CustomerRole) : model.Role.RoleId;
            var newUser = new Users()
            {
                Id = userId,
                Email = model.Email,
                CreatedTime = createdTime,
                UserName = model.UserName,
                PasswordHash = hashedPassword, 
                RoleId = roleId
            };
            await _unitOfWork.Repository<Users>().AddAsync(newUser);
            var newRefreshTokenObj = new RefreshTokens()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Token = refreshToken,
                Expiry = !model.isCustomer ? DateTime.UtcNow.AddDays(1) : DateTime.UtcNow.AddDays(30),
                CreatedAt = createdTime,
                UpdatedAt = createdTime
            };           

            await _unitOfWork.Repository<RefreshTokens>().AddAsync(newRefreshTokenObj);
            var accessToken = GenerateAccessToken(model.Email, model.Role.RoleName);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
            {
                var useResponse = new UserResponse()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    UserName = model.UserName,
                    Email = model.Email
                };

                _logger.LogInformation($"Save user process was done. Total there are {result} records was saved");
                return useResponse;
            }

            return null;
        }

        #region Private
        private string GenerateRefreshToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private bool ValidateRefreshToken(RefreshTokens storedToken, string refreshToken)
        {
            // Kiểm tra token có còn hạn không
            if (storedToken.Expiry < DateTime.UtcNow)
            {
                return false;
            }
            return true;
        }

        private async Task<RefreshTokens?> GetRefreshToken(string refreshToken)
        {
            return await _unitOfWork.Repository<RefreshTokens>().Where(x => x.Token == refreshToken).SingleOrDefaultAsync();
        }

        private async Task<string> RenewRefreshToken(string email, bool isCustomer)
        {
            //update refresh Token
            var newRefreshToken = GenerateRefreshToken();
            var user = await _unitOfWork.Repository<Users>().Where(x => x.Email == email).SingleOrDefaultAsync();
            if (user == null) throw new Exception("User not found");

            var currentRefreshToken = await _unitOfWork.Repository<RefreshTokens>().Where(x => x.UserId == user.Id).SingleOrDefaultAsync();
            if (currentRefreshToken == null) throw new Exception("Refresh token not found");

            var experiedTime = isCustomer ?
                                DateTime.UtcNow.AddMinutes(_authenticationSettings.Value.RefreshTokenExperiedTime.ClientPage) :
                                DateTime.UtcNow.AddMinutes(_authenticationSettings.Value.RefreshTokenExperiedTime.AdminPage);
            currentRefreshToken.Expiry = experiedTime;
            currentRefreshToken.Token = newRefreshToken;
            currentRefreshToken.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Repository<RefreshTokens>().Update(currentRefreshToken);
            var saveDataResult = await _unitOfWork.SaveChangesAsync();

            if (saveDataResult > 0)
            {
                _logger.LogInformation($"Function {nameof(RenewRefreshToken)} was successfully");
                return newRefreshToken;

            }
                _logger.LogInformation($"Function {nameof(RenewRefreshToken)} was unsuccessfully.It returned empty string.");
            return string.Empty;
        }

        private byte[] GenerateSalt(int size = 16)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var salt = new byte[size];
                rng.GetBytes(salt);
                return salt;
            }
        }

        private byte[] HashPassword(string password, byte[] salt, int iterations = 10000, int hashByteSize = 20)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(hashByteSize);
            }
        }

        private string HashPasswordWithSalt(string passwordInput)
        {
            // Generate a new salt
            byte[] salt = GenerateSalt();

            // Hash the password with the salt
            byte[] hash = HashPassword(passwordInput, salt);

            // Combine salt and hash into a single string for storage
            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(string passwordInput, string? storedHashPassword)
        {
            // Split stored hash into salt and password hash
            if (storedHashPassword == null)
            {
                throw new ArgumentNullException("Stored password hash is null");
            }

            var parts = storedHashPassword.Split(':');
            if (parts.Length != 2) return false;

            var salt = Convert.FromBase64String(parts[0]);
            var storedPasswordHash = Convert.FromBase64String(parts[1]);

            // Hash the entered password with the stored salt
            var hash = HashPassword(passwordInput, salt);

            // Compare the entered password's hash to the stored password hash
            return hash.SequenceEqual(storedPasswordHash);
        }

        private async Task<Roles?> GetRole(Guid userId, bool isCustomer)
        {
            var role = await _unitOfWork.Repository<Roles>().AsNoTracking()
                .Where(x => x.Id == userId).FirstOrDefaultAsync();

            return role;
        }

        private async Task<Users?> ValidateUserAsync(string mail, string passwordInput)
        {
            var user = await _unitOfWork.Repository<Users>().AsNoTracking().Include(x => x.Role).FirstOrDefaultAsync(x => x.Email == mail);
            if (user == null) return null;

            var isValidPassword = VerifyPassword(passwordInput, user.PasswordHash);
            if (!isValidPassword) return null;

            return user;
        }

        #endregion



    }
}
