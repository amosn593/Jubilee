using System;
using DAL.DataContext;
using DOMAIN.Models;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpClient("Mpesa", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://sandbox.safaricom.co.ke");
});

builder.Services.Configure<MpesaSetting>(builder.Configuration.GetSection("MpesaSetting"));

builder.Services.AddDbContext<AppDbContext>(
        options => options.UseSqlite("name=ConnectionStrings:DefaultConnection"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
