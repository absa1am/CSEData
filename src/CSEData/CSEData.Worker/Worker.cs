using CSEData.Application.Features.Services;

namespace CSEData.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICompanyService _companyService;

        public Worker(ILogger<Worker> logger, ICompanyService companyService)
        {
            _logger = logger;
            _companyService = companyService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var web = new WebScrabber(@"https://www.cse.com.bd/market/current_price");

                if (web.IsMarketOpen())
                {
                    var data = web.GetData();

                    for (int i = 0; i < data.Count; i++)
                    {
                        dynamic row = data[i];

                        _companyService.Create(row.CompanyCode, row.PriceLTP, row.Open, row.High, row.Low, row.Volume, row.Time);
                    }
                }

                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}