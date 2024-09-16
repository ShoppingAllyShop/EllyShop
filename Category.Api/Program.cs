using Category.Api.Implements;
using Category.Api.Interfaces;
using Comman.Domain.Models;
using Common.Infrastructure.Interfaces;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text;
using CommonLib.Configurations;
using CommonLib.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EllyShopContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("EllyShopDB"));
});

//Add DI
builder.Services.AddTransient<ICategory, CategoryServices>();
builder.Services.AddScoped<IUnitOfWork<EllyShopContext>, UnitOfWork<EllyShopContext>>();

//Add cors
var stringUrls = builder.Configuration.GetSection("AllowedOrigins:Urls").Get<List<string>>().ToArray();
builder.Services.AddCommonCors(stringUrls);

//Authen confg
var secretKey = builder.Configuration["Authentication:SecretKey"];
var issuer = builder.Configuration["Authentication:Issuer"];
var audience = builder.Configuration["Authentication:Audience"];
var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
builder.Services.AddJwtAuthentication(secretKeyBytes, issuer, audience);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(ApiGatewayConstants.CorsPolicyName);
app.UseAuthentication();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();
