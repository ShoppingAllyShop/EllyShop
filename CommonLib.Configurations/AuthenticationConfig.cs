using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CommonLib.Configurations
{
    public static class AuthenticationConfig
    {        
        public static void AddJwtAuthentication(this IServiceCollection services, byte[] secretKeyBytes, string Issuer, string Audience)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(opt =>
                    {
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,  //n?u dùng SSO thì tr? t?i nó
                            ValidateAudience = true,
                            ValidateIssuerSigningKey = true,

                            ValidateLifetime = true, // Ki?m tra th?i gian h?t h?n c?a token
                            ValidIssuer = Issuer,
                            ValidAudience = Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                            ClockSkew = TimeSpan.Zero
                        };
                    });
        }
    }
}
