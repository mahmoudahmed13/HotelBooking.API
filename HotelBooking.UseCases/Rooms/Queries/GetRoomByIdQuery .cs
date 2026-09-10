using HotelBooking.Domain.Common;
using HotelBooking.UseCases.Rooms.DTOs;
using MediatR;

namespace HotelBooking.UseCases.Rooms.Queries
{
    public record GetRoomByIdQuery(int Id) : IRequest<Result<GetRoomByIdResponse>>;
    
}
