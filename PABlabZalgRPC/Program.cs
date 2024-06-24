using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PABlabZalApi.Core.Interfaces;
using PABlabZalApi.Core.Services;
using PABlabZalApi.Infrastructure.Repositories;
using PABlabZalApi.GrpcServices;
using Microsoft.EntityFrameworkCore;
using PABlabZalApi.Infrastructure.Data;
using PABlabZalApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase(databaseName: "IMDB");
});

builder.Services.AddGrpc();

builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ICarRepository, CarRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    SeedData.Initialize(context);
}

// Configure the HTTP request pipeline
app.MapGrpcService<CarServiceImpl>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
