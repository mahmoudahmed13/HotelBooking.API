using HotelBooking.Domain.Entities;
using System.Linq.Expressions;

namespace HotelBooking.Domain.Common
{
    interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
        //Condition , OrderBy , Pagination
    }
}
