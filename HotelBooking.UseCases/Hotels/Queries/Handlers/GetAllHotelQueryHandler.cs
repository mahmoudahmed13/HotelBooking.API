using HotelBooking.Domain.Common;
using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Repositories;
using HotelBooking.UseCases.Hotels.DTOs;
using Mapster;
using MediatR;

namespace HotelBooking.UseCases.Hotels.Queries.Handlers
{
    public class GetAllHotelQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<GetAllHotelQuery, Result<IReadOnlyList<GetAllHotelsResponse>>>
    {
        public async Task<Result<IReadOnlyList<GetAllHotelsResponse>>> Handle(GetAllHotelQuery request, CancellationToken cancellationToken)
        {
            var hotels = await unitOfWork.GetRepository<Hotel, int>()
                .GetAllAsync(cancellationToken);
            var hotelsResponse = hotels.Adapt<IReadOnlyList<GetAllHotelsResponse>>();
            return Result<IReadOnlyList<GetAllHotelsResponse>>.Ok(hotelsResponse);
        }
    }
}
