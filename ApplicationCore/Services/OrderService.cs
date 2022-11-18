using ApplicationCore.Entities;
using ApplicationCore.Entities.BasketAggregate;
using ApplicationCore.Entities.OrderAggregate;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Ardalis.GuardClauses;

namespace ApplicationCore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IUriComposer _uriComposer;
        private readonly IRepository<Basket> _basketRepository;
        private readonly IRepository<Product> _productRepository;

        public OrderService(
            IRepository<Order> orderRepository, 
            IUriComposer uriComposer, 
            IRepository<Basket> basketRepository, 
            IRepository<Product> productRepository)
        {
            _orderRepository = orderRepository;
            _uriComposer = uriComposer;
            _basketRepository = basketRepository;
            _productRepository = productRepository;
        }

        public async Task CreateOrderAsync(int basketId, Address shippingAddress)
        {
            var basketSpec = new BasketWithItemsSpecification(basketId);
            var basket = await _basketRepository.FirstOrDefaultAsync(basketSpec);

            Guard.Against.Null(basket, nameof(basket));
            Guard.Against.EmptyBasketOnCheckout(basket.Items);

            var productsSpecification = new ProductsSpecification(basket.Items.Select(item => item.ProductId).ToArray());
            var products = await _productRepository.ListAsync(productsSpecification);

            var items = basket.Items.Select(basketItem =>
            {
                var product = products.First(c => c.Id == basketItem.ProductId);
                var productOrdered = new ProductOrdered(product.Id, product.Name, _uriComposer.ComposePicUri(product.PictureUri));
                var orderItem = new OrderItem(productOrdered, basketItem.UnitPrice, basketItem.Quantity);
                return orderItem;
            }).ToList();

            var order = new Order(basket.BuyerId, shippingAddress, items);

            await _orderRepository.AddAsync(order);
        }
    }
}
