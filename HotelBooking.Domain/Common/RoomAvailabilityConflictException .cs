namespace HotelBooking.Domain.Common
{

    // Deliberately placed in Domain, not Infrastructure: the Application layer
    // needs to catch this specifically to turn it into a friendly Result, and
    // Application must never reference Infrastructure directly (see the
    // ArchitectureTests rule for Domain - the same direction applies here:
    // Application can depend on Domain, never the other way, and never sideways
    // on Infrastructure).
    public class RoomAvailabilityConflictException : Exception
    {
        public RoomAvailabilityConflictException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
