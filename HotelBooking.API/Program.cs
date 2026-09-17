using HotelBooking.API.Middlewares;
using HotelBooking.Domain.Common;
using HotelBooking.Infrastructure;
using HotelBooking.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddExceptionHandler<GlobalExeptionMiddleware>();
        builder.Services.AddProblemDetails();

        // Add services to the container.
        builder.Services.AddControllers();

        builder.Services.AddApplicationServices();
        builder.Services.AddInfastructureServices(builder.Configuration);
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        })
        .AddMvc();

        builder.Services.AddSwaggerGen(); // generate OpenApi file
        var app = builder.Build();
        // Configure the HTTP request pipeline.
        app.UseExceptionHandler();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        using var scope = app.Services.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("hotel");
        await seeder.SeedDataAsync();

        app.UseHttpsRedirection();
        app.MapControllers();
        app.Run();
    }
}