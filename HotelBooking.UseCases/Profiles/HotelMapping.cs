using HotelBooking.Domain.Entities;
using HotelBooking.UseCases.Hotels.DTOs;
using Mapster;

namespace HotelBooking.UseCases.Profiles
{
    public class HotelMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Hotel, GetAllHotelsResponse>()
                .Map(dest => dest.TotalRoomsCount, src => src.Rooms.Count());
            config.NewConfig<Hotel, GetHotelByIdResponse>()
                .Map(dest => dest.TotalRoomsCount, src => src.Rooms.Count());
        }
    }
}
