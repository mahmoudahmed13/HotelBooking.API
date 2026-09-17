using HotelBooking.Domain.Entities;
using HotelBooking.UseCases.ReservationRooms.DTOs;
using Mapster;

namespace HotelBooking.UseCases.Profiles
{
    public class ReservationRoomsMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ReservationRoom, GetAllReservationRoomsResponse>()
                .Map(dest => dest.Room, src => new[] {$"Id: {src.Room.Id.ToString()}" , $"Room Number: { src.Room.RoomNumber }"})
                .Map(dest => dest.Reservation, src => src.Reservation.GuestFullName);
        }
    }
}
