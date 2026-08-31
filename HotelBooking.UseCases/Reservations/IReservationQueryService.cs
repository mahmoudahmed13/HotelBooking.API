using HotelBooking.UseCases.Reservations.DTOs;

namespace HotelBooking.UseCases.Reservations
{
    public interface IReservationQueryService
    {
        Task<IReadOnlyList<GetAllReservationsResponse>> GetAllReservationsAsync(CancellationToken ct = default);
        Task<GetReservationByIdResponse?> GetReservationByIdAsync(int Id, CancellationToken ct = default);
    }
}
