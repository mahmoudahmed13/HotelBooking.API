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
                .Map(dest => dest.IsAvailable, src => !src.ReservationRooms.Any(
                    rr => rr.Reservation != null &&
                    (rr.Reservation.Status == ReservationStatus.Confirmed ||
                     rr.Reservation.Status == ReservationStatus.Pending ||
                     rr.Reservation.Status == ReservationStatus.CheckedIn)
                    && rr.Reservation.CheckOutDate >= DateOnly.FromDateTime(DateTime.UtcNow)));

            config.NewConfig<Room, GetRoomByIdResponse>()
                .Map(dest => dest.RoomType, src => src.RoomType.ToString())
                .Map(dest => dest.Hotel, src => src.Hotel.Name)
                .Map(dest => dest.GuestName, src => src.ReservationRooms
                    .Where(rr => rr.Reservation != null &&
                                 (rr.Reservation.Status == ReservationStatus.Confirmed ||
                                  rr.Reservation.Status == ReservationStatus.Pending ||
                                  rr.Reservation.Status == ReservationStatus.CheckedIn) &&
                                 rr.Reservation.CheckOutDate >= DateOnly.FromDateTime(DateTime.UtcNow))
                    .Select(rr => rr.Reservation.GuestFullName)
                    .FirstOrDefault());
        }
    }
}
