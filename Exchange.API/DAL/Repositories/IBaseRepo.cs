namespace Exchange.API.DAL.Repositories
{
    public interface IBaseRepo
    {
        Task<decimal> GetRate(string forISO);
    }
}
