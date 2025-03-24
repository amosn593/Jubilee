using System;
using DAL.DataContext;
using DAL.Services.Repository;
using DOMAIN.Interface;
using DOMAIN.Models;
using Hangfire;
using Hangfire.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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

var connstring = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(
        connstring,
        ServerVersion.AutoDetect(connstring)
    );
});

// Add Hangfire services
builder.Services.AddHangfire(config =>
    config.UseStorage(
        new MySqlStorage(
            builder.Configuration.GetConnectionString("HangfireConnection"),
            new MySqlStorageOptions
            {
                //TablePrefix = "Hangfire", // Optional table prefix
                QueuePollInterval = TimeSpan.FromSeconds(15) // Polling interval
            })
    ));
builder.Services.AddHangfireServer();

//builder.Services.AddDbContext<AppDbContext>(
//        options => options.UseSqlite("name=ConnectionStrings:DefaultConnection")
//        options.UseMySql(
//        connstring,
//        ServerVersion.AutoDetect(connstring)
//    );
//        );

builder.Services.AddScoped<ITransactionRepo, TransactionRepo>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
else
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

    app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
