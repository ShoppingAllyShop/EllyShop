using CommonLib.Configurations;
using CommonLib.Constants;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;
using static CommonLib.Constants.AppEnums;

var builder = WebApplication.CreateBuilder(args);
Console.Title = "Api gateway";

var env = builder.Environment;
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
        .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"ocelot.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();

//Authen config
var secretKey = builder.Configuration["Authentication:SecretKey"];
var issuer = builder.Configuration["Authentication:Issuer"];
var audience = builder.Configuration["Authentication:Audience"];
var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
builder.Services.AddJwtAuthentication(secretKeyBytes, issuer, audience);

//Author config
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy(UserRoleEnum.Admin.ToString(), policy => policy.RequireRole(UserRoleEnum.Admin.ToString()));
//});
builder.Services.AddAuthorization();

//Add cors
var stringUrls = builder.Configuration.GetSection("AllowedOrigins:Urls").Get<List<string>>().ToArray();
builder.Services.AddCommonCors(stringUrls);

//Add ocelot
builder.Services.AddOcelot();

var app = builder.Build();

app.UseCors(ApiGatewayConstants.CorsPolicyName);
app.UseAuthentication();
app.UseAuthorization();

app.UseOcelot().Wait();

app.Run();
