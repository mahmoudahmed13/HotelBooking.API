using HotelBooking.UseCases.Hotels.Queries;
using HotelBooking.UseCases.ReservationRooms.Queries;
using HotelBooking.UseCases.Reservations.Queries;
using HotelBooking.UseCases.Rooms.Queries;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HotelBooking.UseCases
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            services.AddScoped<GetAllHotelQuery>();
            services.AddScoped<GetHotelByIdQuery>();
            services.AddScoped<GetAllRoomsQuery>();
            services.AddScoped<GetRoomByIdQuery>();
            services.AddScoped<GetAllReservationsQuery>();
            services.AddScoped<GetReservationByIdQuery>();
            services.AddScoped<GetAllReservationRoomsQuery>();
            return services;
        }
    }
}
