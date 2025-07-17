using Learning.Application.Common.Interfaces;
using Learning.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await _dbSet.FindAsync(new object[] { id }, cancellationToken);

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _dbSet.ToListAsync(cancellationToken);

        public async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
            => await _dbSet.Where(predicate).ToListAsync(cancellationToken);

        public async Task<List<T>> GetWhereWithIncludeAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>> include,
            CancellationToken cancellationToken = default)
        {
            var query = include(_dbSet.AsQueryable());
            return await query.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<List<T>> GetAllWithIncludeAsync(
            Func<IQueryable<T>, IQueryable<T>> include,
            CancellationToken cancellationToken = default)
        {
            var query = include(_dbSet.AsQueryable());
            return await query.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
            => await _dbSet.AddAsync(entity, cancellationToken);

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public void Delete(T entity) => _dbSet.Remove(entity);
        public async Task<T?> GetSingleWithIncludeAsync(
    Expression<Func<T, bool>> predicate,
    Func<IQueryable<T>, IQueryable<T>> include,
    CancellationToken cancellationToken = default)
        {
            var query = include(_dbSet.AsQueryable());
            return await query.FirstOrDefaultAsync(predicate, cancellationToken);
        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync(cancellationToken);
    }
}
