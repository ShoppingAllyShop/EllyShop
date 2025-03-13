using Comman.Domain.Models;
using Common.Infrastructure.Interfaces;
using Common.Infrastructure;
using ContentManagement.API.Implements;
using ContentManagement.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Comman.Domain.Elly_ContentManagement;
using CommonLib.Constants;
using CommonLib.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Console.Title = "Content Management service";

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// ADd DB
builder.Services.AddDbContext<Elly_ContentManagementContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("EllyShopDB"));
});

//Add cors
var stringUrls = builder.Configuration.GetSection("AllowedOrigins:Urls").Get<List<string>>().ToArray();
builder.Services.AddCommonCors(stringUrls);

//Add DI
builder.Services.AddTransient<IContentManagement, ContentManagementService>();
builder.Services.AddScoped<IUnitOfWork<Elly_ContentManagementContext>, UnitOfWork<Elly_ContentManagementContext>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(ApiGatewayConstants.CorsPolicyName);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
