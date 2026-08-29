using HotelBooking.Domain.Common;
using HotelBooking.Infrastructure;
using HotelBooking.UseCases;
using Microsoft.EntityFrameworkCore.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddApplicationServices();
builder.Services.AddInfastructureServices(builder.Configuration);

var app = builder.Build();
// Configure the HTTP request pipeline.

using var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("hotel");
await seeder.SeedDataAsync();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

