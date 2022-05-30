using Microsoft.EntityFrameworkCore;

namespace Exchange.API.DAL.Repositories.Implementations
{
    public class FixerRatesRepository : IFixerRatesRepository
    {
        private readonly ApiDataContext _context;
        public FixerRatesRepository(ApiDataContext context)
        {
            _context = context;
        }
        public async Task<decimal> GetRate(string forISO)
        {
            try
            {
                if (_context.FixerRates.Any(r => r.Currency.Isocode == forISO))
                {
                    var rate = await _context.FixerRates.AsNoTracking().FirstOrDefaultAsync(r => r.Currency.Isocode == forISO);
                    return rate.ToUsdrate;
                }
            }
            catch (Exception)
            {

                throw;
            }

            throw new InvalidOperationException("No Such Rate");
        }
    }
}
