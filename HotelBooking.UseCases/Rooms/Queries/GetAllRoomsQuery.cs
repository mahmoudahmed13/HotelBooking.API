using HotelBooking.Domain.Common;
using HotelBooking.UseCases.Rooms.DTOs;
using MediatR;

namespace HotelBooking.UseCases.Rooms.Queries
{
    public class GetAllRoomsQuery : IRequest<Result<IReadOnlyList<GetAllRoomsResponse>>>;
    
}
