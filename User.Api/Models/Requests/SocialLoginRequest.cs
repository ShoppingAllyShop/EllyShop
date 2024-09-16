using static CommonLib.Constants.AppEnums;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
namespace User.Api.Models.Requests
{
    public class SocialLoginRequest
    {
        public string Token { get; set; }
        public UserRoleEnum Role { get; set; }
        public AuthProvider Provider { get; set; }
    }
}
