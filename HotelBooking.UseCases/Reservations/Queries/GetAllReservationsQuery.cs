using HotelBooking.Domain.Common;
using HotelBooking.UseCases.Reservations.DTOs;
using MediatR;

namespace HotelBooking.UseCases.Reservations.Queries
{
    public record GetAllReservationsQuery : IRequest<Result<IReadOnlyList<GetAllReservationsResponse>>>;
    
}
