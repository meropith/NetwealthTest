namespace Exchange.UI.Services
{
    public interface ICurrencyService
    {
        Task<Dictionary<string, string>> GetCurrencies();
        Task<decimal> Convert(string FromISO, string ToISO, string Provider, string UserTier, decimal amount);
    }
}
