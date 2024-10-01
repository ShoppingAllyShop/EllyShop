using Comman.Domain.Models;
using Common.Infrastructure.Interfaces;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ContentMainPage.Api.Interfaces;
using ContentMainPage.Api.Implements;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ADd DB
builder.Services.AddDbContext<EllyShopContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("EllyShopDB"));
});

//Add DI
builder.Services.AddTransient<IContentMainPage, ContentMainPageService>();
builder.Services.AddScoped<IUnitOfWork<EllyShopContext>, UnitOfWork<EllyShopContext>>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
