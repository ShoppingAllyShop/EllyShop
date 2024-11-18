using User.Api.Models.Abtracts;
using static CommonLib.Constants.AppEnums;

namespace User.Api.Models.Requests
{
    public class UserAuthRequest : UserAccount
    {
        public string Password { get; set; } = string.Empty;
        public bool isCustomer { get; set; }
    }
}
