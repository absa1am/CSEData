using CSEData.Application;
using CSEData.Application.Features.Services;

namespace CSEData.Infrastructure.Features.Services
{
    public class PriceService : IPriceService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;

        public PriceService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
