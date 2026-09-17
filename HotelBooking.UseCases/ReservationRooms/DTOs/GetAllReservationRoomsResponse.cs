namespace HotelBooking.UseCases.ReservationRooms.DTOs
{
    public record GetAllReservationRoomsResponse(int Id, string[] Room, string Reservation);
}
