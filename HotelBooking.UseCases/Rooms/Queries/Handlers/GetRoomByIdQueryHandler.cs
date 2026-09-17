using HotelBooking.Domain.Common;
using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Repositories;
using HotelBooking.UseCases.Rooms.DTOs;
using HotelBooking.UseCases.Rooms.Specifications;
using Mapster;
using MediatR;

namespace HotelBooking.UseCases.Rooms.Queries.Handlers
{
    public class GetRoomByIdQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<GetRoomByIdQuery, Result<GetRoomByIdResponse>>
    {
        public async Task<Result<GetRoomByIdResponse>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new RoomsWithHotelSpec(request.Id);
            var room = await unitOfWork.GetRepository<Room, int>()
                .GetByIdAsync(spec, cancellationToken);
            if (room is null)
                return Error.NotFound("Room not found.");
            return Result<GetRoomByIdResponse>.Ok(room.Adapt<GetRoomByIdResponse>());
        }
    }
}
