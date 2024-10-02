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
using AppUser = Comman.Domain.Models.User;
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
            var user = await _unitOfWork.Repository<AppUser>().FirstOrDefaultAsync(x => x.Email == resultValidateToken.Email);
            if (user == null)
            {
                var createdTime = DateTime.UtcNow;
                newRefreshToken = GenerateRefreshToken();
                //create user
                var userId = Guid.NewGuid();
                var newUser = new AppUser()
                {
                    Id = userId,
                    Email = resultValidateToken.Email,
                    CreatedTime = createdTime,
                    Username = resultValidateToken.Name
                };
                await _unitOfWork.Repository<AppUser>().AddAsync(newUser);
                var newRefreshTokenObj = new RefreshToken()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Token = newRefreshToken,
                    Expiry = DateTime.UtcNow.AddDays(3),
                    CreatedAt = createdTime,
                    UpdatedAt = createdTime
                };

                // Set Role
                var newUserRole = new UserRole()
                {
                    Id = Guid.NewGuid(),
                    RoleId = new Guid(GuidContstants.AdminRole),
                    UserId = userId
                };

                await _unitOfWork.Repository<RefreshToken>().AddAsync(newRefreshTokenObj);
                await _unitOfWork.Repository<UserRole>().AddAsync(newUserRole);
                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("Create new user and stored database successfully");
            }
            else
            {
                newRefreshToken = await RenewRefreshToken(resultValidateToken.Email, true);
            }

            //Tạo access token và result
            var isCustomer = true; //Chỉ client page mới xài social login
            var role = await GetRole(resultValidateToken.Email, isCustomer);
            var accessToken = GenerateAccessToken(resultValidateToken.Email, role.RoleName);
           
            var result = new UserResponse()
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken,
                UserName = user?.Username,
                Email = user?.Username,
                Role = role
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
            var validatedResult = await ValidateUserAsync(model.Email, model.Password);
            if (validatedResult == null)
            {
                throw new ValidationException("Email hoặc mật khẩu không đúng");
            }

            var renewRefreshToken = await RenewRefreshToken(model.Email, model.isCustomer);

            //var user = await _unitOfWork.Repository<AppUser>().AsNoTracking().FirstOrDefaultAsync(x => x.Email == model.Email);
            var role = await GetRole(model.Email, model.isCustomer);

            if (role == null) throw new NullReferenceException("HandleLoginAsync procession was stopped. Role was null");
            return new UserResponse()
            {
                AccessToken = GenerateAccessToken(model.Email, role.RoleName),
                Email = model.Email,
                RefreshToken = renewRefreshToken,
                UserName = validatedResult.Username,
                Role = role
            };
        }
        public async Task<UserResponse?> CreateAccount(UserAuthRequest model)
        {
            var isHasAccount = await _unitOfWork.Repository<AppUser>().AsNoTracking().AnyAsync(x => x.Email == model.Email);
            if (isHasAccount) throw new ValidationException("Email này đã tồn tại");

            //storing database
            string hashedPassword = HashPasswordWithSalt(model.Password);
            var refreshToken = GenerateRefreshToken();
            var createdTime = DateTime.UtcNow;

            //create user
            var userId = Guid.NewGuid();
            var newUser = new AppUser()
            {
                Id = userId,
                Email = model.Email,
                CreatedTime = createdTime,
                Username = model.UserName,
                PasswordHash = hashedPassword
            };
            await _unitOfWork.Repository<AppUser>().AddAsync(newUser);
            var newRefreshTokenObj = new RefreshToken()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Token = refreshToken,
                Expiry = !model.isCustomer ? DateTime.UtcNow.AddDays(1) : DateTime.UtcNow.AddDays(30),
                CreatedAt = createdTime,
                UpdatedAt = createdTime
            };

            // Set Role

            var newUserRole = new UserRole()
            {
                Id = Guid.NewGuid(),
                RoleId = model.Role.RoleId,
                UserId = userId
            };

            await _unitOfWork.Repository<RefreshToken>().AddAsync(newRefreshTokenObj);
            await _unitOfWork.Repository<UserRole>().AddAsync(newUserRole);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
            {
                var accessToken = GenerateAccessToken(model.Email, model.Role.RoleName);
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

        private bool ValidateRefreshToken(RefreshToken storedToken, string refreshToken)
        {
            // Kiểm tra token có còn hạn không
            if (storedToken.Expiry < DateTime.UtcNow)
            {
                return false;
            }
            return true;
        }

        private async Task<RefreshToken?> GetRefreshToken(string refreshToken)
        {
            return await _unitOfWork.Repository<RefreshToken>().Where(x => x.Token == refreshToken).SingleOrDefaultAsync();
        }

        private async Task<string> RenewRefreshToken(string email, bool isCustomer)
        {
            //update refresh Token
            var newRefreshToken = GenerateRefreshToken();
            var user = await _unitOfWork.Repository<AppUser>().Where(x => x.Email == email).SingleOrDefaultAsync();
            if (user == null) throw new Exception("User not found");

            var currentRefreshToken = await _unitOfWork.Repository<RefreshToken>().Where(x => x.UserId == user.Id).SingleOrDefaultAsync();
            if (currentRefreshToken == null) throw new Exception("Refresh token not found");

            var experiedTime = isCustomer ?
                                DateTime.UtcNow.AddMinutes(_authenticationSettings.Value.RefreshTokenExperiedTime.ClientPage) :
                                DateTime.UtcNow.AddMinutes(_authenticationSettings.Value.RefreshTokenExperiedTime.AdminPage);
            currentRefreshToken.Expiry = experiedTime;
            currentRefreshToken.Token = newRefreshToken;
            currentRefreshToken.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Repository<RefreshToken>().Update(currentRefreshToken);
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

        private async Task<RoleModel> GetRole(string email, bool isCustomer)
        {
            //IEnumerable<Role> roles = from u in _unitOfWork.Repository<AppUser>().AsNoTracking()
            //                          join ur in _unitOfWork.Repository<UserRole>().AsNoTracking()
            //                          on u.Id equals ur.UserId
            //                          join r in _unitOfWork.Repository<Role>().AsNoTracking()
            //                          on ur.RoleId equals r.Id
            //                          where u.Email == email
            //                          select r;

            var roles = await _unitOfWork.Repository<UserRole>().AsNoTracking()
                .Include(x => x.Role).Where(x => x.User.Email == email).Select(x => new RoleModel { RoleId = x.Role.Id, RoleName = x.Role.RoleName }).ToListAsync();

            if (isCustomer)
            {
                var customerRole = roles.SingleOrDefault(x => x.RoleName == UserRoleEnum.Customer.ToString());
                if (customerRole == null) throw new NullReferenceException($"{nameof(GetRole)} function was failed.");
                return customerRole;
            }

            var role = roles.SingleOrDefault(x => x.RoleName != UserRoleEnum.Customer.GetEnumDescription());
            if (role == null) throw new NullReferenceException($"{nameof(GetRole)} function was failed.");
            return role;
        }

        private async Task<AppUser?> ValidateUserAsync(string mail, string passwordInput)
        {
            var user = await _unitOfWork.Repository<AppUser>().AsNoTracking().FirstOrDefaultAsync(x => x.Email == mail);
            if (user == null) return null;

            var isValidPassword = VerifyPassword(passwordInput, user.PasswordHash);
            if (!isValidPassword) return null;

            return user;
        }

        #endregion



    }
}
