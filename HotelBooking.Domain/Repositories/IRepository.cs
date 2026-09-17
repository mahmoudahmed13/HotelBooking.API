using Ardalis.Specification;
using HotelBooking.Domain.Entities;

namespace HotelBooking.Domain.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default);
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default);
        Task<TEntity?> GetByIdAsync(ISpecification<TEntity> specification, CancellationToken ct = default);
        Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity> specification, CancellationToken ct = default);
        Task<int> CountAsync(ISpecification<TEntity> spec, CancellationToken ct = default);

    }
}
