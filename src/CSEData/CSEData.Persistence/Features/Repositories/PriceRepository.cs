using CSEData.Application.Features.Repositories;
using CSEData.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSEData.Persistence.Features.Repositories
{
    public class PriceRepository : Repository<Price, int>, IPriceRepository
    {
        public PriceRepository(IApplicationDbContext dbContext) : base((DbContext) dbContext) { }
    }
}
