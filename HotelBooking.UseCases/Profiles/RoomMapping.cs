using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Entities.Enums;
using HotelBooking.UseCases.Hotels.DTOs;
using HotelBooking.UseCases.Rooms.DTOs;
using Mapster;

namespace HotelBooking.UseCases.Profiles
{
    public class RoomMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Room, GetAllRoomsResponse>()
                .Map(dest => dest.RoomType, src => src.RoomType.ToString())
                .Map(dest => dest.Hotel, src => src.Hotel.Name)
                .Map(dest => dest.ReservationRooms, src => src.ReservationRooms.ToList());

            config.NewConfig<Room, GetRoomByIdResponse>()
                .Map(dest => dest.RoomType, src => src.RoomType.ToString())
                .Map(dest => dest.Hotel, src => src.Hotel.Name)
                .Map(dest => dest.ReservationRooms, src => src.ReservationRooms.ToList());
        }
    }
}
