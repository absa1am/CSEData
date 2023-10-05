namespace CSEData.Application.Features.Services
{
    public interface ICompanyService
    {
        int GetCompanyId(string companyName);
        void Create(string companyCode, string priceLTP, string open, string high, string low, string volume, DateTime time);
    }
}
