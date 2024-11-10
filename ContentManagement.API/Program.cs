using Comman.Domain.Models;
using Common.Infrastructure.Interfaces;
using Common.Infrastructure;
using ContentManagement.API.Implements;
using ContentManagement.API.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Console.Title = "Content Management service";

// ADd DB
builder.Services.AddDbContext<EllyShopContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("EllyShopDB"));
});

//Add DI
builder.Services.AddTransient<IContentManagement, ContentManagementService>();
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
