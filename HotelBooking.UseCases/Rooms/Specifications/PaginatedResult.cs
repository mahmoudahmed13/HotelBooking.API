namespace HotelBooking.UseCases.Rooms.Specifications
{
    public class PaginatedResult<TEntity>
    {
        public PaginatedResult(int pageIndex, int pageSize, int count, IReadOnlyList<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
            TotalPages = pageSize > 0
                ? (int)Math.Ceiling(count / (double)pageSize)
                : 0;
        }

        public int PageIndex { get; }
        public int PageSize { get; }
        public int Count { get; }
        public int TotalPages { get; }
        public IReadOnlyList<TEntity> Data { get; }
    }
}
