using Exchange.API.DAL.Repositories;

namespace Exchange.API.DAL.Services.Implementations
{
    public class ConvertService : IConvertService
    {
        private readonly IExternalAPIsService _externalAPIsService;
        private readonly IExchangeRatesRepository _exchangeRatesRepository;
        private readonly IFixerRatesRepository _fixerRatesRepository;
        private readonly ICurrenciesRepository _currenciesRepository;
        public ConvertService(IExternalAPIsService ExternalAPIsService, IExchangeRatesRepository exchangeRatesRepository, IFixerRatesRepository fixerRatesRepository, ICurrenciesRepository currenciesRepository)
        {
            _externalAPIsService = ExternalAPIsService;
            _fixerRatesRepository = fixerRatesRepository;
            _exchangeRatesRepository = exchangeRatesRepository;
            _currenciesRepository = currenciesRepository;
        }

        public async Task<decimal> Convert(string fromISO, string toISO, string provider, string userTier, decimal amount)
        {
            try
            {
                if (userTier == "TierDB" || userTier == "FREE")
                {
                    if (provider == "Fixer")
                    {
                        var rate = await _fixerRatesRepository.GetRate(fromISO);
                        var toUSD = amount / rate;

                        rate = await _fixerRatesRepository.GetRate(toISO);
                        var result = toUSD * rate;

                        return result;
                    }

                    if (provider == "Exchangerate")
                    {
                        var rate = await _exchangeRatesRepository.GetRate(fromISO);
                        var toUSD = amount / rate;

                        rate = await _exchangeRatesRepository.GetRate(toISO);
                        var result = toUSD * rate;

                        return result;
                    }

                }

                if (userTier == "TierAPI")
                {
                    if (provider == "Fixer")
                    {
                        var result = await _externalAPIsService.FixerConvert(fromISO, toISO, amount);
                        return result.Result;
                    }

                    if (provider == "Exchangerate")
                    {
                        var result = await _externalAPIsService.ExchangeRates(fromISO, toISO, amount);
                        return result.Result;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            throw new InvalidOperationException("Invalid Data selection");
        }

        public async Task<Dictionary<string, string>> GetCurrencies()
        {
            try
            {
                return await _currenciesRepository.GetCurrenciesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
