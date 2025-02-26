using Comman.Domain.Elly_User;
using System.Reflection.PortableExecutable;

namespace User.Api.Models.Responses
{
    public class DataAdminUserPageResponse
    {
        public ContentPageData? ContentPageData { get; set; }

        public SearchEmployeeUserResponse? UserData { get; set; }
    }

    public class ContentPageData
    {
        public IEnumerable<Position>? Positions { get; set; }
        public IEnumerable<Department>? Departments { get; set; }
        public IEnumerable<Roles>? Roles { get; set; }
    }

}
