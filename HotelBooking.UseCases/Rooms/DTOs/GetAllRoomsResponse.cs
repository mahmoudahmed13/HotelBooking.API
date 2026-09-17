using HotelBooking.Domain.Entities;

namespace HotelBooking.UseCases.Rooms.DTOs
{
    public record GetAllRoomsResponse(int Id, string RoomType, string RoomNumber, int Capacity, string Hotel, bool IsAvailable);

}
