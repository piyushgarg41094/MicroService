
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace Mango.Services.ShoppingCartAPI.Utility
{
    //DelegatingHandler are similar to .NET Core Middleware but the main difference is that DelegatingHandler are on client side
    public class BackendApiAuthenticationHttpClientHandler : DelegatingHandler 
    {
        private readonly IHttpContextAccessor _accessor;
        public BackendApiAuthenticationHttpClientHandler(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _accessor.HttpContext.GetTokenAsync("access_token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",token);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
