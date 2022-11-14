using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;

namespace Web.Middlewares
{
    public class TransferAnonymousBasketToUserMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICurrentUserService _currentUserService;

        public TransferAnonymousBasketToUserMiddleware(
            RequestDelegate next, 
            ICurrentUserService currentUserService)
        {
            _next = next;
            _currentUserService = currentUserService;
        }

        public async Task InvokeAsync(HttpContext context, IBasketService _basketService)
        {
            if (context.User.Identity.IsAuthenticated && context.Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                var anonymousId = context.Request.Cookies[Constants.BASKET_COOKIENAME];
                if (Guid.TryParse(anonymousId, out var _))
                {
                    Guard.Against.NullOrEmpty(_currentUserService.Id, nameof(_currentUserService.Id));
                    await _basketService.TransferBasketAsync(anonymousId, _currentUserService.Id);
                }
                context.Response.Cookies.Delete(Constants.BASKET_COOKIENAME);
            }

            // Call the next delegate/middleware in the pipeline.
            await _next(context);
        }
    }

    public static class TransferAnonymousBasketToUserMiddlewareExtensions
    {
        public static IApplicationBuilder UseTransferAnonymousBasketToUser(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TransferAnonymousBasketToUserMiddleware>();
        }
    }
}
