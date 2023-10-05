using CSEData.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace CSEData.Persistence
{
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TKey : IComparable where TEntity : class, IEntity<TKey>
    {
        protected DbContext _dbContext;
        protected DbSet<TEntity> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void Edit(TEntity entityToEdit)
        {
            _dbSet.Attach(entityToEdit);
            _dbContext.Entry(entityToEdit).State = EntityState.Modified;
        }

        public virtual async Task EditAsync(TEntity entityToEdit)
        {
            await Task.Run(() =>
            {
                _dbSet.Attach(entityToEdit);
                _dbContext.Entry(entityToEdit).State = EntityState.Modified;
            });
        }

        public virtual IList<TEntity> GetAll()
        {
            IQueryable<TEntity> query = _dbSet;

            return query.ToList();
        }

        public virtual async Task<IList<TEntity>> GetAllAsync()
        {
            IQueryable<TEntity> query = _dbSet;

            return await query.ToListAsync();
        }

        public virtual TEntity Get(TKey id)
        {
            return _dbSet.Find(id);
        }

        public virtual async Task<TEntity> GetAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual IList<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool isTrackingOff = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            if (include != null)
                query = include(query);

            if (orderBy != null)
            {
                var result = orderBy(query);

                if (isTrackingOff)
                    return result.AsNoTracking().ToList();

                return result.ToList();
            }
            else
            {
                if (isTrackingOff)
                    return query.AsNoTracking().ToList();

                return query.ToList();
            }
        }

        public virtual async Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool isTrackingOff = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            if (include != null)
                query = include(query);

            if (orderBy != null)
            {
                var result = orderBy(query);

                if (isTrackingOff)
                    return await result.AsNoTracking().ToListAsync();

                return await result.ToListAsync();
            }
            else
            {
                if (isTrackingOff)
                    return await query.AsNoTracking().ToListAsync();

                return await query.ToListAsync();
            }
        }

        public virtual (IList<TEntity> data, int total, int totalDisplay) Get(Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false)
        {
            IQueryable<TEntity> query = _dbSet;
            int total = query.Count();
            int totalDisplay = total;
            int skipSize = (pageIndex - 1) * pageSize;

            if (predicate != null)
                query = query.Where(predicate);

            if (include != null)
                query = include(query);

            if (orderBy != null)
            {
                var result = orderBy(query).Skip(skipSize).Take(pageSize);

                if (isTrackingOff)
                    return (result.AsNoTracking().ToList(), total, totalDisplay);

                return (result.ToList(), total, totalDisplay);
            }
            else
            {
                var result = query.Skip(skipSize).Take(pageSize);

                if (isTrackingOff)
                    return (result.AsNoTracking().ToList(), total, totalDisplay);

                return (result.ToList(), total, totalDisplay);
            }
        }

        public virtual async Task<(IList<TEntity> data, int total, int totalDisplay)> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false)
        {
            IQueryable<TEntity> query = _dbSet;
            int total = query.Count();
            int totalDisplay = total;
            int skipSize = (pageIndex - 1) * pageSize;

            if (predicate != null)
                query = query.Where(predicate);

            if (include != null)
                query = include(query);

            IList<TEntity> data;

            if (orderBy != null)
            {
                var result = orderBy(query).Skip(skipSize).Take(pageSize);

                if (isTrackingOff)
                    data = await result.AsNoTracking().ToListAsync();
                else data = await result.ToListAsync();
            }
            else
            {
                var result = query.Skip(skipSize).Take(pageSize);

                if (isTrackingOff)
                    data = await result.AsNoTracking().ToListAsync();
                else data = await result.ToListAsync();
            }

            return (data, total, totalDisplay);
        }

        public virtual IList<TEntity> Get(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            if (include != null)
                query = include(query);

            return query.ToList();
        }

        public virtual async Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            if (include != null)
                query = include(query);

            return await query.ToListAsync();
        }

        public virtual IList<TEntity> GetDynamic(Expression<Func<TEntity, bool>> predicate = null,
        string orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool isTrackingOff = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            if (include != null)
                query = include(query);

            if (orderBy != null)
            {
                var result = query.OrderBy(orderBy);

                if (isTrackingOff)
                    return result.AsNoTracking().ToList();

                return result.ToList();
            }
            else
            {
                if (isTrackingOff)
                    return query.AsNoTracking().ToList();

                return query.ToList();
            }
        }

        public virtual async Task<IList<TEntity>> GetDynamicAsync(Expression<Func<TEntity, bool>> predicate = null,
        string orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool isTrackingOff = false)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            if (include != null)
                query = include(query);

            if (orderBy != null)
            {
                var result = query.OrderBy(orderBy);

                if (isTrackingOff)
                    return await result.AsNoTracking().ToListAsync();

                return await result.ToListAsync();
            }
            else
            {
                if (isTrackingOff)
                    return await query.AsNoTracking().ToListAsync();

                return await query.ToListAsync();
            }
        }

        public virtual (IList<TEntity> data, int total, int totalDisplay) GetDynamic(Expression<Func<TEntity, bool>> predicate = null,
        string orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false)
        {
            IQueryable<TEntity> query = _dbSet;
            var total = query.Count();
            var totalDisplay = total;
            var skipSize = (pageIndex - 1) * pageSize;

            if (predicate != null)
            {
                query = query.Where(predicate);
                totalDisplay = query.Count();
            }

            if (include != null)
                query = include(query);

            if (orderBy != null)
            {
                var result = query.OrderBy(orderBy).Skip(skipSize).Take(pageSize);

                if (isTrackingOff)
                    return (result.AsNoTracking().ToList(), total, totalDisplay);

                return (result.ToList(), total, totalDisplay);
            }
            else
            {
                var result = query.Skip(skipSize).Take(pageSize);

                if (isTrackingOff)
                    return (result.AsNoTracking().ToList(), total, totalDisplay);

                return (result.ToList(), total, totalDisplay);
            }
        }

        public virtual async Task<(IList<TEntity> data, int total, int totalDisplay)> GetDynamicAsync(Expression<Func<TEntity, bool>> predicate = null,
        string orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false)
        {
            IQueryable<TEntity> query = _dbSet;
            var total = query.Count();
            var totalDisplay = total;
            var skipSize = (pageIndex - 1) * pageSize;

            if (predicate != null)
            {
                query = query.Where(predicate);
                totalDisplay = query.Count();
            }

            if (include != null)
                query = include(query);

            IList<TEntity> data;

            if (orderBy != null)
            {
                var result = query.OrderBy(orderBy).Skip(skipSize).Take(pageSize);

                if (isTrackingOff)
                    data = await result.AsNoTracking().ToListAsync();
                else data = await result.ToListAsync();
            }
            else
            {
                var result = query.Skip(skipSize).Take(pageSize);

                if (isTrackingOff)
                    data = await result.AsNoTracking().ToListAsync();
                else data = await result.ToListAsync();
            }

            return (data, total, totalDisplay);
        }

        public virtual async Task<IEnumerable<TResult>> GetAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true, CancellationToken cancellationToken = default) where TResult : class
        {
            var query = _dbSet.AsQueryable();

            if (disableTracking)
                query.AsNoTracking();

            if (include is not null)
                query = include(query);

            if (predicate is not null)
                query = query.Where(predicate);

            return orderBy is not null ? await orderBy(query).Select(selector!).ToListAsync(cancellationToken) : await query.Select(selector!).ToListAsync(cancellationToken);
        }

        public virtual async Task<TResult> SingleOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = true)
        {
            var query = _dbSet.AsQueryable();

            if (disableTracking)
                query.AsNoTracking();

            if (include is not null)
                query = include(query);

            if (predicate is not null)
                query = query.Where(predicate);

            return (orderBy is not null ? await orderBy(query).Select(selector!).FirstOrDefaultAsync() : await query.Select(selector!).FirstOrDefaultAsync());
        }

        public virtual void Remove(TEntity entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
                _dbSet.Attach(entityToDelete);

            _dbSet.Remove(entityToDelete);
        }

        public virtual async Task RemoveAsync(TEntity entityToDelete)
        {
            await Task.Run(() =>
            {
                if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
                    _dbSet.Attach(entityToDelete);

                _dbSet.Remove(entityToDelete);
            });
        }

        public virtual void Remove(TKey id)
        {
            var entityToDelete = _dbSet.Find(id);

            Remove(entityToDelete);
        }

        public virtual async Task RemoveAsync(TKey id)
        {
            var entityToDelete = _dbSet.Find(id);

            await RemoveAsync(entityToDelete);
        }

        public virtual void Remove(Expression<Func<TEntity, bool>> predicate)
        {
            var entitiesToDelete = _dbSet.Where(predicate);

            _dbSet.RemoveRange(entitiesToDelete);
        }

        public virtual async Task RemoveAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entitiesToDelete = _dbSet.Where(predicate);

            await Task.Run(() =>
            {
                _dbSet.RemoveRange(entitiesToDelete);
            });
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
                query = _dbSet.Where(predicate);

            return query.Count();
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            return await query.CountAsync();
        }
    }
}
