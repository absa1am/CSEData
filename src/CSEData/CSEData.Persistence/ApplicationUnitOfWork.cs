using CSEData.Application;
using CSEData.Application.Features.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CSEData.Persistence
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public ICompanyRepository Companies { get; set; }
        public IPriceRepository Prices { get; set; }

        public ApplicationUnitOfWork(IApplicationDbContext dbContext, ICompanyRepository companyRepository, IPriceRepository priceRepository) : base((DbContext) dbContext)
        {
            Companies = companyRepository;
            Prices = priceRepository;
        }
    }
}
