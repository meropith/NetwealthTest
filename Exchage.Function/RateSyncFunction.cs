using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exchage.Function.DAL;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Exchage.Function
{
    public class RateSyncFunction
    {
        private readonly ApiDataContext _context;

        public RateSyncFunction(ApiDataContext context)
        {
            _context = context;
        }

        [FunctionName("RateSyncFunction")]
        public async Task RunAsync([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");


            RestClient _RClient = new RestClient(new Uri("https://api.apilayer.com/"));
            _RClient.AddDefaultHeader("Content-Type", "application/json");
            var request = new RestRequest($"fixer/latest?base=USD");
            request.AddHeader("apikey", "S9JZb2JybPYXxCcN70tOkwNc7KXGJXVB");
            var varResult = await _RClient.GetAsync(request);

            var root = JObject.Parse(varResult.Content);
            var rateValues = root["rates"].ToObject<Dictionary<string, string>>();

            var Currencies = _context.Currencies.ToList();
            Currencies.ForEach(rate => rate.IsSupported = false);
            await _context.SaveChangesAsync();

            foreach (var rate in rateValues.Where(v => !v.Value.Contains("-")))
            {
                var dbCurrency = _context.Currencies.FirstOrDefault(c => c.Isocode == rate.Key);
                if (dbCurrency == null)
                {
                    //Currency Is not supported, could be add with something like:
                    /*
                    var newRate = _context.Currencies.Add(new Models.DB.Currency() { Isocode = rate.Key, Name = rate.Key, IsSupported = true });
                    _context.SaveChanges();

                    _context.FixerRates.Add(new Models.DB.FixerRate()
                    {
                        CurrencyId = newRate.Entity.Id,
                        ToUsdrate = Convert.ToDecimal(rate.Value)
                    });
                    _context.SaveChanges();*/

                }
                else
                {
                    dbCurrency.IsSupported = true;
                    var cDbRate = _context.FixerRates.FirstOrDefault(r => r.CurrencyId == dbCurrency.Id);
                    if (cDbRate == null)
                    {
                        _context.FixerRates.Add(new Models.DB.FixerRate
                        {
                            CurrencyId = dbCurrency.Id,
                            ToUsdrate = Convert.ToDecimal(rate.Value)
                        });
                    }
                    else
                    {
                        cDbRate.ToUsdrate = Convert.ToDecimal(rate.Value);
                    }
                }
            }
            await _context.SaveChangesAsync();


            Currencies = _context.Currencies.Where(c => c.IsSupported).ToList();
            Currencies.ForEach(rate => rate.IsSupported = false);
            await _context.SaveChangesAsync();

            request = new RestRequest($"exchangerates_data/latest?base=USD");
            request.AddHeader("apikey", "S9JZb2JybPYXxCcN70tOkwNc7KXGJXVB");
            varResult = await _RClient.GetAsync(request);

            root = JObject.Parse(varResult.Content);
            rateValues = root["rates"].ToObject<Dictionary<string, string>>();

            foreach (var rate in rateValues.Where(v => !v.Value.Contains("-")))
            {
                var dbCurrency = _context.Currencies.FirstOrDefault(c => c.Isocode == rate.Key);
                if (dbCurrency == null)
                {
                    //Currency Is not supported, could be add with something like:
                    /*
                    var newRate = _context.Currencies.Add(new Models.DB.Currency() { Isocode = rate.Key, Name = rate.Key, IsSupported = true });
                    _context.SaveChanges();

                    _context.FixerRates.Add(new Models.DB.FixerRate()
                    {
                        CurrencyId = newRate.Entity.Id,
                        ToUsdrate = Convert.ToDecimal(rate.Value)
                    });
                    _context.SaveChanges();*/

                }
                else
                {
                    dbCurrency.IsSupported = true;
                    var cDbRate = _context.ExchangeRates.FirstOrDefault(r => r.CurrencyId == dbCurrency.Id);
                    if (cDbRate == null)
                    {
                        _context.ExchangeRates.Add(new Models.DB.ExchangeRate
                        {
                            CurrencyId = dbCurrency.Id,
                            ToUsdrate = Convert.ToDecimal(rate.Value)
                        });
                    }
                    else
                    {
                        cDbRate.ToUsdrate = Convert.ToDecimal(rate.Value);
                    }
                }
            }
            await _context.SaveChangesAsync();


        }
    }
}
