using Comman.Domain.Elly_User;
using User.Api.Models.Requests;

namespace User.Api.UnitTest.InitData
{
    public static class MockData
    {
        private static readonly string Mail1 = "Test1@gmail.com";
        private static readonly string PassInput1 = "123456";
        public static List<Department> CreateDepartmentList()
        {
            return new List<Department>
               {
                   new Department
                   {
                       Id = Guid.NewGuid(),
                       DepartmentName = "Department 1",
                       IsActive = true
                   },
                   new Department
                   {
                       Id = Guid.NewGuid(),
                       DepartmentName = "Department 2",
                       IsActive = true
                   },
               };
        }

        public static List<Roles> CreateRoleList()
        {
            return new List<Roles>
               {
                   new Roles
                   {
                       Id = Guid.NewGuid(),
                       RoleName = "Role 1"
                   },
                   new Roles
                   {
                       Id = Guid.NewGuid(),
                       RoleName = "Role 2"
                   },
               };
        }

        public static List<Position> CreatePositionList()
        {
            return new List<Position>
               {
                   new Position
                   {
                       Id = Guid.NewGuid(),
                       PositionName = "Position 1",
                   },
                   new Position
                   {
                       Id = Guid.NewGuid(),
                       PositionName = "Position 2"
                   },
               };
        }

        public static UserAuthRequest CreateUserAuthRequest()
        {
            return new UserAuthRequest
            {
                Email = Mail1,
                Password = PassInput1
            };
        }
    }
}
