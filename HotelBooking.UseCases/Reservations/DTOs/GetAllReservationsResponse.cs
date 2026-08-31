namespace HotelBooking.UseCases.Reservations.DTOs
{
    public record GetAllReservationsResponse(int Id, DateOnly BookingDate, DateOnly CheckInDate, DateOnly CheckOutDate,
        string Status, decimal TotalPrice, int NumberOfAdults, int NumberOfChildren,
        string GuestFullName, string GuestEmail, string GuestPhone);
}
