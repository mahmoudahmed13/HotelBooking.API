using HotelBooking.Domain.Entities;

namespace HotelBooking.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
        Task<int> SaveChangeAsync(CancellationToken ct = default);
    }
}
