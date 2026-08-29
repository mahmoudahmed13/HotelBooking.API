using Microsoft.Extensions.DependencyInjection;

namespace HotelBooking.UseCases
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
