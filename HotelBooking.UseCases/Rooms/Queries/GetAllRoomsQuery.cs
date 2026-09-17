using HotelBooking.Domain.Common;
using HotelBooking.UseCases.Rooms.DTOs;
using HotelBooking.UseCases.Rooms.Specifications;
using MediatR;

namespace HotelBooking.UseCases.Rooms.Queries
{
    public record GetAllRoomsQuery(RoomQueryParams QueryParams) : IRequest<Result<PaginatedResult<GetAllRoomsResponse>>>;
    
}
