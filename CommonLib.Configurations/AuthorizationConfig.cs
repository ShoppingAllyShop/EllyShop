using CommonLib.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CommonLib.Constants.AppEnums;

namespace CommonLib.Configurations
{
    public static class AuthorizationConfig
    {
        public static void AddRoleAuthorization(this IServiceCollection services)
        {

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole(UserRoleEnum.Admin.GetEnumDescription()));
            });
        }
    }
}
