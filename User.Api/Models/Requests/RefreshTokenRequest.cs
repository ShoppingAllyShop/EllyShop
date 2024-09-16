using User.Api.Models.Abtracts;
using static CommonLib.Constants.AppEnums;

namespace User.Api.Models.Requests
{
    public class RefreshTokenRequestModel: UserAccount
    {
        public string RefreshToken { get; set; }
    }
}
