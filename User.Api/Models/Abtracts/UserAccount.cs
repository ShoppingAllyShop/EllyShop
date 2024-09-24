using static CommonLib.Constants.AppEnums;

namespace User.Api.Models.Abtracts
{
    public abstract class UserAccount 
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public RoleModel Role { get; set; } = new RoleModel();
    }
}
