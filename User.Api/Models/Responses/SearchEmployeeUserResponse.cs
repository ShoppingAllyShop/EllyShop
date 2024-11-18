using CommonLib.Models.Base;

namespace User.Api.Models.Responses
{
    public class SearchEmployeeUserResponse
    {
        public PagingResponseBase? Paging { get; set; }
        public IEnumerable<UserInfo>? UserList { get; set; }
    }

    public class UserInfo
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public Guid PositionId { get; set; }
        public Guid DepartmentId { get; set; }
        public string Department { get; set; }
        public string RoleName { get; set; }
        public Guid RoleId { get; set; }
        public string Phone { get; set; }
    }


}
