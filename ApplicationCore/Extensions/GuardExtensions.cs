using ApplicationCore.Entities.BasketAggregate;
using ApplicationCore.Exceptions;

namespace ApplicationCore.Extensions
{
    public static class GuardExtensions
    {
        public static void EmptyBasketOnCheckout(this IGuardClause guardClause, IReadOnlyCollection<BasketItem> basketItems)
        {
            if (!basketItems.Any())
                throw new EmptyBasketOnCheckoutException();
        }
    }
}
