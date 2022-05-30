namespace Exchange.API.DAL.Services
{
    public interface IConvertService
    {
        Task<decimal> Convert(string FromISO, string ToISO, string Provider, string UserTier, decimal amount);
        Task<Dictionary<string,string>> GetCurrencies();
    }
}
