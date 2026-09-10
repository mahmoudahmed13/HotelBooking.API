using HotelBooking.Domain.Common;
using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Repositories;
using HotelBooking.UseCases.Rooms.DTOs;
using Mapster;
using MediatR;

namespace HotelBooking.UseCases.Rooms.Queries.Handlers
{
    public class GetAllRoomsQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<GetAllRoomsQuery, Result<IReadOnlyList<GetAllRoomsResponse>>>
    {
        public async Task<Result<IReadOnlyList<GetAllRoomsResponse>>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            var rooms = await unitOfWork.GetRepository<Room, int>()
                .GetAllAsync(cancellationToken);
            var roomsResponse = rooms.Adapt<IReadOnlyList<GetAllRoomsResponse>>();
            return Result<IReadOnlyList<GetAllRoomsResponse>>.Ok(roomsResponse);
        }
    }
}
