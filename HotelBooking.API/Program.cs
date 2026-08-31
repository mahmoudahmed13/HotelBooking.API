using HotelBooking.API.Middlewares;
using HotelBooking.Domain.Common;
using HotelBooking.Infrastructure;
using HotelBooking.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalExeptionMiddleware>();
builder.Services.AddProblemDetails();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddApplicationServices();
builder.Services.AddInfastructureServices(builder.Configuration);

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseExceptionHandler();  

using var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("hotel");
await seeder.SeedDataAsync();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

