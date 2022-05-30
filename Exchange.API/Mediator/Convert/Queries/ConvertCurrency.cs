using Exchange.API.DAL.Services;
using Exchange.API.Models;
using MediatR;
namespace Exchange.API.Mediator.Convert.Queries
{
    public record ConvertCurrencyQuery : IRequest<ApiResponse<decimal>>, ICacheable
    {
        public string FromISO { get; set; } = String.Empty;
        public string ToISO { get; set; } = String.Empty;
        public string Provider { get; set; } = String.Empty;
        public string UserTier { get; set; } = String.Empty;
        public decimal Amount { get; set; }
        public string CacheKey => "C" + Provider + "-" + FromISO + "-" + ToISO + "-" + Amount;
    }


    public class ConvertCurrency : IRequestHandler<ConvertCurrencyQuery, ApiResponse<decimal>>
    {
        private readonly IConvertService _convertService;

        public ConvertCurrency(IConvertService ConvertService)
        {
            _convertService = ConvertService;
        }

        public async Task<ApiResponse<decimal>> Handle(ConvertCurrencyQuery request, CancellationToken cancellationToken)
        {
            var result = await _convertService.Convert(request.FromISO, request.ToISO, request.Provider, request.UserTier, request.Amount);
            return new ApiResponse<decimal>(result);
        }
    }
}
