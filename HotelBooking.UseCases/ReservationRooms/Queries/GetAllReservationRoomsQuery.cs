using HotelBooking.Domain.Common;
using HotelBooking.UseCases.ReservationRooms.DTOs;
using MediatR;

namespace HotelBooking.UseCases.ReservationRooms.Queries
{
    public record GetAllReservationRoomsQuery(int? reservationId, int? roomId, string? searchValue) : IRequest<Result<IReadOnlyList<GetAllReservationRoomsResponse>>>;

    
}
