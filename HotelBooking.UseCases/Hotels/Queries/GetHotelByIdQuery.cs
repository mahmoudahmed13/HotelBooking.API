using HotelBooking.Domain.Common;
using HotelBooking.UseCases.Hotels.DTOs;
using MediatR;

namespace HotelBooking.UseCases.Hotels.Queries
{
    public record GetHotelByIdQuery(int Id) : IRequest<Result<GetHotelByIdResponse>>;
    
}
