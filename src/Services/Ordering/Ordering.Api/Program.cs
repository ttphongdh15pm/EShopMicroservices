var builder = WebApplication.CreateBuilder(args);
// Add services to the container

builder.Services
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationService()
    .AddApiServices();

var app = builder.Build();
// Configure the HTTP request pipeline

app.Run();
