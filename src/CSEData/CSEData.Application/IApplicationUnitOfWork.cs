using CSEData.Application.Features.Repositories;
using CSEData.Domain.UnitOfWorks;

namespace CSEData.Application
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        ICompanyRepository Companies { get; set; }
        IPriceRepository Prices { get; set; }
    }
}
