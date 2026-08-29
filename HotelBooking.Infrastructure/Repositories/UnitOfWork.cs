using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Repositories;
using HotelBooking.Infrastructure.Data;
using System.Collections.Concurrent;

namespace HotelBooking.Infrastructure.Repositories
{
    public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
    {
        private readonly ConcurrentDictionary<Type, object> _repositories = new();
        public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            return (IRepository<TEntity, TKey>)_repositories.GetOrAdd(
                typeof(TEntity), _ => new Repository<TEntity, TKey>(dbContext));
        }

        public async Task<int> SaveChangeAsync(CancellationToken ct = default)
            => await dbContext.SaveChangesAsync(ct);
    }
}
