using CSEData.Domain.Entities;
using System.Linq.Expressions;

namespace CSEData.Domain.Repositories
{
    public interface IRepositoryBase<TEntity, TKey> where TEntity : class, IEntity<TKey> where TKey : IComparable
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);

        void Edit(TEntity entityToEdit);
        Task EditAsync(TEntity entityToEdit);

        void Remove(TEntity entityToRemove);
        Task RemoveAsync(TEntity entityToRemove);
        void Remove(TKey id);
        Task RemoveAsync(TKey id);
        void Remove(Expression<Func<TEntity, bool>> predicate);
        Task RemoveAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Get(TKey id);
        Task<TEntity> GetAsync(TKey id);
        IList<TEntity> GetAll();
        Task<IList<TEntity>> GetAllAsync();

        int Count(Expression<Func<TEntity, bool>> predicate = null);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
    }
}
