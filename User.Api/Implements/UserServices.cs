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

        public string GenerateAccessToken(string email, UserRoleEnum role)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_authenticationSettings.Value.SecretKey);
            var secretKey = new SymmetricSecurityKey(secretKeyBytes);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(JwtRegisteredClaimNames.Email, email ?? string.Empty),
                    //roles
                    new Claim(ClaimTypes.Role, role.GetEnumDescription())
                }),
                //Expires = pageType == ConmonConstants.AdminPage ? DateTime.UtcNow.AddDays(1) : DateTime.UtcNow.AddDays(10),
                //Expires = role != Roles.Customer ? DateTime.UtcNow.AddDays(1) : DateTime.UtcNow.AddDays(10),
                Expires = role != UserRoleEnum.Customer ? DateTime.UtcNow.AddMinutes(1) : DateTime.UtcNow.AddMinutes(1),
                Issuer = _authenticationSettings.Value.Issuer,
                Audience = _authenticationSettings.Value.Audience,
                SigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            return accessToken;
        }

        private string GenerateRefreshToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private bool ValidateRefreshToken(RefreshToken? storedToken, string refreshToken)
        {
            // Kiểm tra token tồn tại và khớp
            if (storedToken == null || storedToken.Token != refreshToken)
            {
                return false;
            }

            // Kiểm tra token có còn hạn không
            if (storedToken.Expiry < DateTime.UtcNow)
            {
                return false;
            }
            return true;
        }

        //private async Task<ValidateTokenOAuthResponse?> validateFacebookToken(string token)
        //{
        //    var appToken = $"{_facebookSettings.Value.AppId}|{_facebookSettings.Value.AppSecret}";
        //    var validateTokenFacebookUrl = string.Format(UserConstant.ValidateTokenFacebookUrl, token, appToken);
        //    var validateTokenResult = await _apiServices.GetAsync<ValidateTokenOAuthResponse>(validateTokenFacebookUrl);
        //    return validateTokenResult;
        //}

        //private async Task<ValidateTokenOAuthResponse?> validateGoogleToken(string token)
        //{
        //    var googleRes = await GoogleJsonWebSignature.ValidateAsync(token);
        //    return new ValidateTokenOAuthResponse()
        //    {
        //        Email = googleRes.Email,
        //        Name = googleRes.Name
        //    };

        //}

        //private async Task<ValidateTokenOAuthResponse?> ValidateTokenOAuth(string token, AuthProvider provider)
        //{
        //    var result = new ValidateTokenOAuthResponse();
        //    switch (provider)
        //    {
        //        case AuthProvider.Facebook:
        //            result = await validateFacebookToken(token);
        //            break;
        //        case AuthProvider.Google:
        //            result = await validateGoogleToken(token);
        //            break;
        //        default:
        //            break;
        //    }
        //    return result;
        //}

        public async Task<UserResponse> HandleSocialLogin(SocialLoginRequest request)
        {
            //Validate access token of Google, Facebook....
            //var validateToken = await ValidateTokenOAuth(request.Credential, request.Provider);
            //if (validateToken == null) throw new Exception("validate token of social login failed. Return null result");
            
            var validator = _validatorFactory.CreateValidator(request.Provider);
            var resultValidateToken = await validator.ValidateToken(request.Token);
            if (resultValidateToken == null) throw new Exception("validate token of social login failed. Return null result");

            // Kiểm tra người dùng trong DB hoặc tạo mới nếu cần
            var newRefreshToken = string.Empty;
            var isHasAccount = await _unitOfWork.Repository<AppUser>().AnyAsync(x => x.Email == resultValidateToken.Email);
            if (!isHasAccount)
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
                _logger.LogInformation("Create new user successfully");
            }
            else
            {
                newRefreshToken = await RenewRefreshToken(resultValidateToken.Email, true);
                _logger.LogInformation("Renew refresh token successfully");
            }   

            //Tạo access token và result
            var accessToken = GenerateAccessToken(resultValidateToken.Email, request.Role);

            //Tạo response            
            //var role = await (from u in _unitOfWork.Repository<AppUser>().AsNoTracking()
            //                  join ur in _unitOfWork.Repository<UserRole>().AsNoTracking()
            //                  on u.Id equals ur.UserId
            //                  join r in _unitOfWork.Repository<Role>().AsNoTracking()
            //                  on ur.RoleId equals r.Id
            //                  where u.Email == resultValidateToken.Email
            //                  select r).ToListAsync();

            var result = new UserResponse()
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken,
                UserName = resultValidateToken.Name,
                Email = resultValidateToken.Email
            };
            return result;
        }       

        private Guid GetRoleId(UserRoleEnum roleEnum)
        {
            switch (roleEnum)
            {
                case UserRoleEnum.Customer:
                    return new Guid(GuidContstants.CustomerRole);
                case UserRoleEnum.Admin:
                    return new Guid(GuidContstants.AdminRole);
                default:
                    return Guid.Empty;
            }
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

            currentRefreshToken.Expiry = isCustomer ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddDays(1);
            currentRefreshToken.Token = newRefreshToken;
            currentRefreshToken.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Repository<RefreshToken>().Update(currentRefreshToken);
            var saveDataResult = await _unitOfWork.SaveChangesAsync();

            if (saveDataResult > 0)
            {
                return newRefreshToken;
            }

            return string.Empty;
        }

        public async Task<string> RefreshToken(RefreshTokenRequestModel requestModel)
        {
            //validate refresh token
            var storedToken = await GetRefreshToken(requestModel.RefreshToken);
            var isValidRefreshToken = ValidateRefreshToken(storedToken, requestModel.RefreshToken);
            if (storedToken == null || !isValidRefreshToken) return string.Empty;

            //create new access token
            var newAccessToken = GenerateAccessToken(requestModel.Email, requestModel.Role);
            return newAccessToken;
        }

        public async Task<UserResponse> HandleLoginAsync(UserAuthRequest model)
        {
            var isValidUser = await ValidateUserAsync(model.Email, model.Password);
            if (!isValidUser)
            {
                throw new ValidationException("Email hoặc mật khẩu không đúng");
            }
            
            var renewRefreshToken = await RenewRefreshToken(model.Email, model.isCustomer);
            _logger.LogInformation("Renew refresh token successfully");

            return new UserResponse()
            {
                AccessToken = GenerateAccessToken(model.Email, model.Role),
                Email = model.Email,
                RefreshToken = renewRefreshToken,
                UserName = model.UserName
            };
        }

        private async Task<bool> ValidateUserAsync(string mail, string passwordInput)
        {
            var user = await _unitOfWork.Repository<AppUser>().AsNoTracking().FirstOrDefaultAsync(x => x.Email == mail);
            if (user == null) return false;

            var isValidPassword = VerifyPassword(passwordInput, user.PasswordHash);
            if (!isValidPassword) return false;

            return true;
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
                Expiry = model.Role != UserRoleEnum.Customer ? DateTime.UtcNow.AddDays(1) : DateTime.UtcNow.AddDays(30),
                CreatedAt = createdTime,
                UpdatedAt = createdTime
            };

            // Set Role

            var newUserRole = new UserRole()
            {
                Id = Guid.NewGuid(),
                RoleId = GetRoleId(model.Role),
                UserId = userId
            };

            await _unitOfWork.Repository<RefreshToken>().AddAsync(newRefreshTokenObj);
            await _unitOfWork.Repository<UserRole>().AddAsync(newUserRole);
            var result = await _unitOfWork.SaveChangesAsync();           

            if (result > 0)
            {
                var accessToken = GenerateAccessToken(model.Email, model.Role);
                var useResponse = new UserResponse()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    RoleName = model.Role.GetEnumDescription(),
                    UserName = model.UserName,
                    Email = model.Email
                };

                _logger.LogInformation($"Save user process was done. Total there are {result} records was saved");
                return useResponse;
            }

            return null;
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
    }
}
