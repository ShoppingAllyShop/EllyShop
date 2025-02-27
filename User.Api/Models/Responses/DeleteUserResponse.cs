namespace User.Api.Models.Responses
{
    public class DeleteUserResponse
    {
        public SearchEmployeeUserResponse PagingUserList { get; set; }
        public string UserName { get; set; }
    }
}
