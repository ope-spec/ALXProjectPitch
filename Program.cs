using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ProjectPitch4.Models;
using ProjectPitch4.Services;
using System.Reflection;
using System.IO;
using System;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Core.Configuration;
using ProjectPitch4.models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ALXPortfolioProject",
        Version = "v1",

    });
    var xmlFile = "ALXPortfolioProject.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

});

builder.Services.Configure<AppConfig>(builder.Configuration.GetSection(nameof(AppConfig)));
builder.Services.AddSingleton<MongoService>();
builder.Services.AddSingleton<HttpServer>();
builder.Services.AddSingleton<MySqlConnectionService>();

var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
builder.Services.AddDbContext<MySqlContext>(
dbContextOptions => dbContextOptions
    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ALXPortfolioProject v1");
        c.RoutePrefix = "";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
