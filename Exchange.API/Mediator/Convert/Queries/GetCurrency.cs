using Exchange.API.DAL.Services;
using Exchange.API.Models;
using MediatR;
namespace Exchange.API.Mediator.Convert.Queries
{
    public record GetCurrencyQuery : IRequest<ApiResponse<Dictionary<string,string>>>, ICacheable
    {
        public string CacheKey => "CCurrencies";
        public string UserTier => "";
    }

    public class GetCurrency : IRequestHandler<GetCurrencyQuery, ApiResponse<Dictionary<string, string>>>
    {
        private readonly IConvertService _convertService;

        public GetCurrency(IConvertService ConvertService)
        {
            _convertService = ConvertService;
        }

        public async Task<ApiResponse<Dictionary<string, string>>> Handle(GetCurrencyQuery request, CancellationToken cancellationToken)
        {
            var result = await _convertService.GetCurrencies();
            return new ApiResponse<Dictionary<string, string>> (result);
        }
    }
}
