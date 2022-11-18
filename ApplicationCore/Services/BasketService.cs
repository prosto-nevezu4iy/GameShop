using ApplicationCore.Entities.BasketAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Ardalis.GuardClauses;
using Ardalis.Result;

namespace ApplicationCore.Services
{
    public class BasketService : IBasketService
    {
        private readonly IRepository<Basket> _basketRepository;

        public BasketService(IRepository<Basket> basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<Basket> AddItemToBasket(Guid userId, int productId, decimal price, byte quantity = 1)
        {
            var basketSpec = new BasketWithItemsSpecification(userId);
            var basket = await _basketRepository.FirstOrDefaultAsync(basketSpec);

            if (basket == null)
            {
                basket = new Basket(userId);
                await _basketRepository.AddAsync(basket);
            }

            basket.AddItem(productId, price, quantity);

            await _basketRepository.UpdateAsync(basket);
            return basket;
        }

        public async Task DeleteBasketAsync(int basketId)
        {
            var basket = await _basketRepository.GetByIdAsync(basketId);
            Guard.Against.Null(basket, nameof(basket));
            await _basketRepository.DeleteAsync(basket);
        }

        public async Task SetQuantities(int basketId, Dictionary<string, byte> quantities)
        {
            var basketSpec = new BasketWithItemsSpecification(basketId);
            var basket = await _basketRepository.FirstOrDefaultAsync(basketSpec);
            if (basket == null) return;

            foreach (var item in basket.Items)
            {
                if (quantities.TryGetValue(item.Id.ToString(), out var quantity))
                {
                    item.SetQuantity(quantity);
                }
            }

            basket.RemoveEmptyItems();
            await _basketRepository.UpdateAsync(basket);
        }

        public async Task TransferBasketAsync(string anonymousId, string userId)
        {
            var anonymousBasketSpec = new BasketWithItemsSpecification(Guid.Parse(anonymousId));
            var anonymousBasket = await _basketRepository.FirstOrDefaultAsync(anonymousBasketSpec);
            if (anonymousBasket == null) return;
            var userBasketSpec = new BasketWithItemsSpecification(Guid.Parse(userId));
            var userBasket = await _basketRepository.FirstOrDefaultAsync(userBasketSpec);
            if (userBasket == null)
            {
                userBasket = new Basket(Guid.Parse(userId));
                await _basketRepository.AddAsync(userBasket);
            }
            foreach (var item in anonymousBasket.Items)
            {
                userBasket.AddItem(item.ProductId, item.UnitPrice, item.Quantity);
            }
            await _basketRepository.UpdateAsync(userBasket);
            await _basketRepository.DeleteAsync(anonymousBasket);
        }
    }
}
