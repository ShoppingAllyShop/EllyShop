using CommonLib.Constants;
using Microsoft.Extensions.DependencyInjection;


namespace CommonLib.Configurations
{
    public static class SecurityCors
    {
        public static void AddCommonCors(this IServiceCollection services, string[] urlList)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: ApiGatewayConstants.CorsPolicyName,
                                  policy =>
                                  {
                                      policy.WithOrigins(urlList)
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .AllowCredentials()
                                      .SetIsOriginAllowed(x => true);
                                  });
            });            
        }
    }
}
