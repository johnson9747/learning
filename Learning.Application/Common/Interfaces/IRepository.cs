using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Application.Common.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<T>> GetAllWithIncludeAsync(
            Func<IQueryable<T>, IQueryable<T>> include,
            CancellationToken cancellationToken = default);
        Task<List<T>> GetWhereAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default);
        Task<List<T>> GetWhereWithIncludeAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> include,
            CancellationToken cancellationToken = default);
        Task<T?> GetSingleWithIncludeAsync(
   Expression<Func<T, bool>> predicate,
   Func<IQueryable<T>, IQueryable<T>> include,
   CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        T Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
