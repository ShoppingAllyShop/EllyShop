using Category.Api.Implements;
using Category.Api.Interfaces;
using Comman.Domain.Models;
using Common.Infrastructure.Interfaces;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text;
using CommonLib.Configurations;
using CommonLib.Constants;
using AutoMapper;
using Category.Api.Mapper;
using Microsoft.Extensions.DependencyInjection;
using Catalog.Api.Interfaces;
using Catalog.Api.Implements;
using Comman.Domain.Elly_Catalog;

var builder = WebApplication.CreateBuilder(args);
Console.Title = "Catalog service";

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Elly_CatalogContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("EllyShopDB"));
});

//Add DI
builder.Services.AddTransient<ICategory, CategoryServices>();
builder.Services.AddTransient<IProduct, ProductService>();
builder.Services.AddTransient<ICollections, CollectionService>();
builder.Services.AddTransient<ICatalog, CatalogService>();
builder.Services.AddScoped<IUnitOfWork<Elly_CatalogContext>, UnitOfWork<Elly_CatalogContext>>();

//Add cors
var stringUrls = builder.Configuration.GetSection("AllowedOrigins:Urls").Get<List<string>>().ToArray();
builder.Services.AddCommonCors(stringUrls);

//Authen confg
var secretKey = builder.Configuration["Authentication:SecretKey"];
var issuer = builder.Configuration["Authentication:Issuer"];
var audience = builder.Configuration["Authentication:Audience"];
var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
builder.Services.AddJwtAuthentication(secretKeyBytes, issuer, audience);

//Add automapper
builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseCors(ApiGatewayConstants.CorsPolicyName);
app.UseAuthentication();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();
