using HotelBooking.Domain.Common;
using HotelBooking.Domain.Repositories;
using HotelBooking.Infrastructure.Data;
using HotelBooking.Infrastructure.DataSeeding;
using HotelBooking.Infrastructure.Interceptors;
using HotelBooking.Infrastructure.Queries;
using HotelBooking.Infrastructure.Repositories;
using HotelBooking.UseCases.Hotels;
using HotelBooking.UseCases.ReservationRooms;
using HotelBooking.UseCases.Reservations;
using HotelBooking.UseCases.Rooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBooking.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfastructureServices(this IServiceCollection services, IConfiguration configration) 
        {
            var concurrencyInterceptor = new ConcurrencyConflictInterceptor();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configration.GetConnectionString("DefaultConnection"))
                        .EnableSensitiveDataLogging()
                        .AddInterceptors(concurrencyInterceptor);
            });

            services.AddKeyedScoped<IDataSeeder, HotelDataSeeder>("hotel");
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IHotelQueryService, HotelQueryServices>();
            services.AddScoped<IRoomQueryService, RoomQueryService>();
            services.AddScoped<IReservationQueryService, ReservationQueryService>();
            services.AddScoped<IReservationRoomQueryService, ReservationRoomQueryService>();
            return services;
        }
    }
}
