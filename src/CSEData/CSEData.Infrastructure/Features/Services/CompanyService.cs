using CSEData.Application;
using CSEData.Application.Features.Services;
using CSEData.Domain.Entities;

namespace CSEData.Infrastructure.Features.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;

        public CompanyService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int GetCompanyId(string companyCode)
        {
            var company = _unitOfWork.Companies.GetAll()
                                               .Where(c => c.CompanyCode == companyCode)
                                               .FirstOrDefault();

            return company.Id;
        }

        public void Create(string companyCode, string priceLTP, string open, string high, string low, string volume, DateTime time)
        {
            var prices = new Price()
            {
                PriceLTP = priceLTP,
                Volume = volume,
                Open = open,
                High = high,
                Low = low,
                Time = time
            };

            if (_unitOfWork.Companies.IsDuplicateCompanyCode(companyCode))
                prices.CompanyId = GetCompanyId(companyCode);
            else
            {
                var company = new Company() { CompanyCode = companyCode };

                prices.Company = company;
            }

            _unitOfWork.Prices.Add(prices);
            _unitOfWork.Save();
        }
    }
}
