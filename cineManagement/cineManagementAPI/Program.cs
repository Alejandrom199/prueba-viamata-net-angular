using cineManagementDatabaseFirst.Contexts;
using cineManagementDatabaseFirst.Models.DTOs;
using cineManagementDatabaseFirst.Repository;
using cineManagementDatabaseFirst.Repository.impl;
using cineManagementDatabaseFirst.Services;
using cineManagementDatabaseFirst.Services.impl;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CineManagementDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Add CORS policy to allow requests from the Angular app
builder.Services.AddCors(
    options =>
    {
        options.AddPolicy("AllowAngularApp",
            builder => builder
                .WithOrigins("http://localhost:4200") // Replace with your Angular app URL
                .AllowAnyMethod()
                .AllowAnyHeader()
        );
    }
);

// Configuración de repositorios
builder.Services.AddScoped<IPeliculaRepository, PeliculaRepository>();
builder.Services.AddScoped<ISalaCineRepository, SalaCineRepository>();
builder.Services.AddScoped<IPeliculaSalaCineRepository, PeliculaSalaCineRepository>();

// Configuración de servicios
builder.Services.AddScoped<IPeliculaService, PeliculaService>();
builder.Services.AddScoped<ISalaCineService, SalaCineService>();
builder.Services.AddScoped<IPeliculaSalaCineService, PeliculaSalaCineService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS for the Angular app
app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
