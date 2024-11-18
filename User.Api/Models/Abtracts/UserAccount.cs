using static CommonLib.Constants.AppEnums;

namespace User.Api.Models.Abtracts
{
    public abstract class UserAccount 
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public Guid RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}
