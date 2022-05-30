using Exchange.API.Models.Responces;

namespace Exchange.API.DAL.Services
{
    public interface IExternalAPIsService
    {
        Task<FixerConvertResponce> FixerConvert(string from, string to, decimal amout);
        Task<ExchangeRatesResponce> ExchangeRates(string from, string to, decimal amout);
    }
}
