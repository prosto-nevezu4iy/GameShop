using ApplicationCore.Entities.BasketAggregate;
using Ardalis.Result;

namespace ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        Task TransferBasketAsync(string anonymousId, string userId);
        Task<Basket> AddItemToBasket(Guid userId, int productId, decimal price, byte quantity = 1);
        Task<Result<Basket>> SetQuantities(int basketId, Dictionary<string, byte> quantities); // <<Result<Basket>>
        Task DeleteBasketAsync(int basketId);
    }
}
