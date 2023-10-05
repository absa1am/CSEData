using CSEData.Domain.Entities;
using CSEData.Domain.Repositories;

namespace CSEData.Application.Features.Repositories
{
    public interface ICompanyRepository : IRepositoryBase<Company, int>
    {
        bool IsDuplicateCompanyCode(string companyCode);
    }
}
