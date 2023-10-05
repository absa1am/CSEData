using CSEData.Application.Features.Repositories;
using CSEData.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSEData.Persistence.Features.Repositories
{
    public class CompanyRepository : Repository<Company, int>, ICompanyRepository
    {
        public CompanyRepository(IApplicationDbContext dbContext) : base((DbContext) dbContext) { }

        public bool IsDuplicateCompanyCode(string companyCode)
        {
            int companies = Count(c => c.CompanyCode == companyCode);

            return companies > 0;
        }
    }
}
