using Microsoft.EntityFrameworkCore;

namespace Exchange.API.DAL.Repositories.Implementations
{
    public class CurrenciesRepository : ICurrenciesRepository
    {

        private readonly ApiDataContext _context;
        public CurrenciesRepository(ApiDataContext context)
        {
            _context = context;
        }
        public async Task<Dictionary<string, string>> GetCurrenciesAsync()
        {
            try
            {
                return await _context.Currencies.AsNoTracking().Where(c => c.IsSupported).ToDictionaryAsync(c => c.Isocode, c => c.Name);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
