using HotelBooking.Domain.Common;
using HotelBooking.UseCases.Reservations.DTOs;
using MediatR;

namespace HotelBooking.UseCases.Reservations.Queries
{
    public record GetReservationByIdQuery(int Id) : IRequest<Result<GetReservationByIdResponse>>;
    
}
