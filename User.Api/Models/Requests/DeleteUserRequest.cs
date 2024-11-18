using CommonLib.Models.Base;

namespace User.Api.Models.Requests
{
    public class DeleteUserRequest: PagingSearchRequestBase
    {
        public Guid UserId { get; set; }
    }
}
