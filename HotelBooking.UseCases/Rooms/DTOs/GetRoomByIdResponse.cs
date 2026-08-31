using HotelBooking.Domain.Entities;

namespace HotelBooking.UseCases.Rooms.DTOs
{
    public record GetRoomByIdResponse(int Id, string RoomType, string RoomNumber, int Capacity, string Hotel, IReadOnlyList<ReservationRoom> ReservationRooms);
}
