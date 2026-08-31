using HotelBooking.Domain.Common;
using HotelBooking.UseCases.Reservations.DTOs;

namespace HotelBooking.UseCases.Reservations.Queries
{
    public class GetAllReservationsQuery(IReservationQueryService reservationQueryService)
    {
        public async Task<Result<IReadOnlyList<GetAllReservationsResponse>>> GetAllReservationsAsync(CancellationToken ct = default)
            => Result<IReadOnlyList<GetAllReservationsResponse>>
                .Ok(await reservationQueryService.GetAllReservationsAsync(ct));
    }
}
