using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Constants
{
    public static class AppEnums
    {
        public enum AuthProvider
        {
            [Description("Facebook")]
            Facebook = 0,
            [Description("Google")]
            Google = 1
        }

        public enum UserRoleEnum
        {
            [Description("Customer")]
            Customer = 0,
            [Description("Admin")]
            Admin = 1
        }
    }
}
