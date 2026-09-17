using HotelBooking.Domain.Common;
using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Repositories;
using HotelBooking.UseCases.Rooms.DTOs;
using HotelBooking.UseCases.Rooms.Specifications;
using Mapster;
using MediatR;

namespace HotelBooking.UseCases.Rooms.Queries.Handlers
{
    public class GetAllRoomsQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<GetAllRoomsQuery, Result<PaginatedResult<GetAllRoomsResponse>>>
    {
        public async Task<Result<PaginatedResult<GetAllRoomsResponse>>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            var spec = new RoomsWithHotelSpec(request.QueryParams);
            var rooms = await unitOfWork.GetRepository<Room, int>()
                .GetAllAsync(spec, cancellationToken);
            var roomsResponse = rooms.Adapt<IReadOnlyList<GetAllRoomsResponse>>();

            // 4. Return Paginated Result
            var countSpec = new RoomsCountSpecifications(request.QueryParams);
            var CountOfAllRooms = await unitOfWork.GetRepository<Room, int>().CountAsync(countSpec, cancellationToken);
            var result = new PaginatedResult<GetAllRoomsResponse>(
                request.QueryParams.PageIndex,
                request.QueryParams.PageSize,
                CountOfAllRooms,
                roomsResponse);

            return Result<PaginatedResult<GetAllRoomsResponse>>.Ok(result);
        }
    }
}
