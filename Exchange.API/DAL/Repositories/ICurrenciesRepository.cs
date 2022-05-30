namespace Exchange.API.DAL.Repositories
{
    public interface ICurrenciesRepository
    {
        Task<Dictionary<string,string>> GetCurrenciesAsync();
    }
}
