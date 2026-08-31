using HotelBooking.UseCases.Hotels.DTOs;

namespace HotelBooking.UseCases.Hotels
{
    public interface IHotelQueryService
    {
        Task<IReadOnlyList<GetAllHotelsResponse>> GetAllHotelsAsync(CancellationToken ct = default);
        Task<GetHotelByIdResponse?> GetHotelByIdAsync(int Id, CancellationToken ct = default);
    }
}
