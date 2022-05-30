using Exchange.API.Models.Responces;
using Newtonsoft.Json;
using RestSharp;

namespace Exchange.API.DAL.Services.Implementations
{
    public class ExternalAPIsService : IExternalAPIsService
    {
        private readonly ApiSettings _apiSettings;
        private RestClient _RClient;
        public ExternalAPIsService(ApiSettings apiSettings)
        {
            _apiSettings = apiSettings;

        }

        public async Task<FixerConvertResponce> FixerConvert(string from, string to, decimal amount)
        {
            try
            {
                _RClient = new RestClient(new Uri(_apiSettings.FixerApiBaseURL));
                _RClient.AddDefaultHeader("Content-Type", "application/json");
                var request = new RestRequest($"fixer/convert?to={to}&from={from}&amount={amount}");
                request.AddHeader("apikey", _apiSettings.FixerApiKey);
                var varResult = await _RClient.GetAsync(request);
                FixerConvertResponce Deserialized = JsonConvert.DeserializeObject<FixerConvertResponce>(varResult.Content);
                return Deserialized;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ExchangeRatesResponce> ExchangeRates(string from, string to, decimal amount)
        {
            try
            {

                _RClient = new RestClient(new Uri(_apiSettings.ExchangeRatesBaseURL));
                _RClient.AddDefaultHeader("Content-Type", "application/json");
                var request = new RestRequest($"exchangerates_data/convert?to={to}&from={from}&amount={amount}");
                request.AddHeader("apikey", _apiSettings.FixerApiKey);
                var varResult = await _RClient.GetAsync(request);
                ExchangeRatesResponce Deserialized = JsonConvert.DeserializeObject<ExchangeRatesResponce>(varResult.Content);
                return Deserialized;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
