using CSEData.Domain.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace CSEData.Persistence
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual void Dispose()
        {
            _dbContext?.Dispose();
        }

        public virtual void Save()
        {
            _dbContext?.SaveChanges();
        }

        public virtual async Task SaveAsync()
        {
            _dbContext?.SaveChangesAsync();
        }
    }
}
