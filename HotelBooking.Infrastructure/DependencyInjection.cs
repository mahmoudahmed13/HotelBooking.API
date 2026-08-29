using HotelBooking.Domain.Common;
using HotelBooking.Infrastructure.Data;
using HotelBooking.Infrastructure.DataSeeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBooking.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfastructureServices(this IServiceCollection services, IConfiguration configration) 
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configration.GetConnectionString("DefaultConnection"))
                        .EnableSensitiveDataLogging();
            });

            services.AddKeyedScoped<IDataSeeder, HotelDataSeeder>("hotel");
            return services;    
        }
    }
}
