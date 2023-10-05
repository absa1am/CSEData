using CSEData.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSEData.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Company> Companies { get; set; }
        DbSet<Price> Prices { get; set; }
    }
}
