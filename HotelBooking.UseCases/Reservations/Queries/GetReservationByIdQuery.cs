using HotelBooking.Domain.Common;
using HotelBooking.UseCases.Reservations.DTOs;

namespace HotelBooking.UseCases.Reservations.Queries
{
    public class GetReservationByIdQuery(IReservationQueryService reservationQueryService)
    {
        public async Task<Result<GetReservationByIdResponse>> GetReservationByIdAsync(int Id, CancellationToken ct)
        {
            var reservation = await reservationQueryService.GetReservationByIdAsync(Id, ct);
            if (reservation == null)
                Error.NotFound("Reservation not found");
            return Result<GetReservationByIdResponse>.Ok(reservation);
        }
    }
}
