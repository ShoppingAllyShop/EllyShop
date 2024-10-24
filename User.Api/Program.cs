using Comman.Domain.Elly_User;
using Common.Infrastructure;
using Common.Infrastructure.Interfaces;
using CommonLib.Configurations;
using CommonLib.Constants;
using CommonLib.Helpers.Implements;
using CommonLib.Helpers.Interfaces;
using CommonLib.Models.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using User.Api.Implements;
using User.Api.Implements.Factory;
using User.Api.Implements.TokenValidator;
using User.Api.Interfaces;
using User.Api.Interfaces.Factory;
using User.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

//Add DB
builder.Services.AddDbContext<Elly_UserContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("EllyShopDB"));
});

//Add appsetting json
builder.Services.Configure<AuthenticationSetting>(builder.Configuration.GetSection("Authentication"));
builder.Services.Configure<ApiGatewaySetting>(builder.Configuration.GetSection("ApiGateway"));
builder.Services.Configure<FacebookSetting>(builder.Configuration.GetSection("Facebook"));
builder.Services.Configure<GoogleSettings>(builder.Configuration.GetSection("Google"));

//Add DI
builder.Services.AddTransient<IUser, UserServices>();
builder.Services.AddTransient<IApiServices, ApiServices>();
builder.Services.AddTransient<ITokenValidatorFactory, TokenValidatorFactory>();
builder.Services.AddScoped<IUnitOfWork<Elly_UserContext>, UnitOfWork<Elly_UserContext>>();
builder.Services.AddTransient<GoogleTokenValidator>();
builder.Services.AddTransient<FacebookTokenValidator>();

//Add cors
var stringUrls = builder.Configuration.GetSection("AllowedOrigins:Urls").Get<List<string>>().ToArray();
builder.Services.AddCommonCors(stringUrls);

//Authen confg
var secretKey = builder.Configuration["Authentication:SecretKey"];
var issuer = builder.Configuration["Authentication:Issuer"];
var audience = builder.Configuration["Authentication:Audience"];
var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
builder.Services.AddJwtAuthentication(secretKeyBytes, issuer, audience);
builder.Services.AddAuthorization();


var app = builder.Build();
app.UseCors(ApiGatewayConstants.CorsPolicyName);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
//app.UseHttpsRedirection();
app.Run();
