using HotelBooking.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HotelBooking.Infrastructure.Interceptors
{
    // Catches DbUpdateConcurrencyException - the technical, EF-Core-specific
// exception thrown when a RowVersion mismatch is detected - and replaces it
// with RoomAvailabilityConflictException, a message the Application layer
// (and eventually the API's GlobalExceptionHandler) can understand and
// map to a proper 409 Conflict without knowing anything about EF Core.
public class ConcurrencyConflictInterceptor : SaveChangesInterceptor
{
    private const string ConflictMessage =
        "This room was just booked by someone else for overlapping dates. Please pick a different room or time.";
 
    public override void SaveChangesFailed(DbContextErrorEventData eventData)
    {
        if (eventData.Exception is DbUpdateConcurrencyException concurrencyException)
            throw new RoomAvailabilityConflictException(ConflictMessage, concurrencyException);
 
        base.SaveChangesFailed(eventData);
    }
 
    public override Task SaveChangesFailedAsync(
        DbContextErrorEventData eventData,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Exception is DbUpdateConcurrencyException concurrencyException)
            throw new RoomAvailabilityConflictException(ConflictMessage, concurrencyException);
 
        return base.SaveChangesFailedAsync(eventData, cancellationToken);
    }
}
}
