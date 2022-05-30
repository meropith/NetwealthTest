using Exchange.API.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Exchange.API.Mediator.Behaviours
{
    public class CachingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
         where TResponse : Response        
    {
        private readonly IMemoryCache cache;
        private readonly ApiSettings _APISettings;
        public CachingBehaviour(IMemoryCache cache, ApiSettings apiSettings)
        {
            this.cache = cache;
            _APISettings = apiSettings;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {        
            
            var r = request as ICacheable;
            TResponse response;
            
            if (r.UserTier == "FREE")
            {                                
                if (_APISettings.UseCache == true && cache.TryGetValue(r.CacheKey, out response))
                {
                    
                    return response;
                }
            }

            response = await next();
            if (_APISettings.UseCache)
            {
                cache.Set(r.CacheKey, response);
            }
            return response;
        }

    }
}
