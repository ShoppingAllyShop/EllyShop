namespace User.Api.Models.Requests
{
    public class CreateUpdateUserRequest
    {
        public Guid RoleId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid PositionId { get; set; }
        public string UserName { get; set; }
        public string mail { get; set; }
    }
}
