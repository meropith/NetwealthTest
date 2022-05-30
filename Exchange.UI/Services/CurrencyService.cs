using Blazored.LocalStorage;
using Exchange.UI.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Exchange.UI.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        public CurrencyService(HttpClient client, ILocalStorageService localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }

        public async Task<decimal> Convert(string FromISO, string ToISO, string Provider, string UserTier, decimal amount)
        {
            var token = await _localStorage.GetItemAsync<string>("JWT Token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var content = JsonConvert.SerializeObject(new ConvertRequest() { 
            Amount = amount,
            FromISO = FromISO,
            Provider = Provider,
            ToISO = ToISO
            });
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("Convert/Convert", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse<decimal>>(contentTemp);
            return result.Data;
        }

        public async Task<Dictionary<string, string>> GetCurrencies()
        {
            var token = await _localStorage.GetItemAsync<string>("JWT Token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _client.GetAsync("Convert/GetCurrencies");
            var contentTemp = await response.Content.ReadAsStringAsync();            
            var result = JsonConvert.DeserializeObject<ApiResponse<Dictionary<string,string>>>(contentTemp);
            return result.Data;
        }
    }
}
