using Microsoft.EntityFrameworkCore;
using SmartLibrary.Api;
using SmartLibrary.Api.Data;
using SmartLibrary.Api.Repositories;
using SmartLibrary.Api.Services;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// 1. Налаштування БД (Connection string має бути в appsettings.json)
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Реєстрація Repositories та Services (Dependency Injection)
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

// 3. Реєстрація AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();