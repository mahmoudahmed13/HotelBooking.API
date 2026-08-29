using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Repositories;
using HotelBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Repositories
{
    public class Repository<TEntity, TKey>(AppDbContext dbContext)
        : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public void Add(TEntity entity) => dbContext.Set<TEntity>().Add(entity);
        public void Delete(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);
        public void Update(TEntity entity) => dbContext.Set<TEntity>().Update(entity);
        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
            => await dbContext.Set<TEntity>().ToListAsync(ct);

        public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default)
            => await dbContext.Set<TEntity>().FindAsync(id, ct);

    }
}
