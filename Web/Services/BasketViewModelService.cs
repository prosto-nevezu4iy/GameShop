using ApplicationCore.Entities;
using ApplicationCore.Entities.BasketAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class BasketViewModelService : IBasketViewModelService
    {
        private readonly IRepository<Basket> _basketRepository;
        private readonly IUriComposer _uriComposer;
        private readonly IBasketQueryService _basketQueryService;
        private readonly IRepository<Product> _productRepository;

        public BasketViewModelService(
            IRepository<Basket> basketRepository, 
            IUriComposer uriComposer, 
            IBasketQueryService basketQueryService, 
            IRepository<Product> productRepository)
        {
            _basketRepository = basketRepository;
            _uriComposer = uriComposer;
            _basketQueryService = basketQueryService;
            _productRepository = productRepository;
        }

        public async Task<int> CountTotalBasketItems(Guid userId)
        {
            var counter = await _basketQueryService.CountTotalBasketItems(userId);

            return counter;
        }

        public async Task<BasketViewModel> GetOrCreateBasketForUser(Guid userId)
        {
            var basketSpec = new BasketWithItemsSpecification(userId);
            var basket = (await _basketRepository.FirstOrDefaultAsync(basketSpec));

            if (basket == null)
            {
                return await CreateBasketForUser(userId);
            }

            var viewModel = await Map(basket);
            return viewModel;
        }

        private async Task<BasketViewModel> CreateBasketForUser(Guid userId)
        {
            var basket = new Basket(userId);
            await _basketRepository.AddAsync(basket);

            return new BasketViewModel()
            {
                BuyerId = basket.BuyerId,
                Id = basket.Id,
            };
        }

        private async Task<List<BasketItemViewModel>> GetBasketItems(IReadOnlyCollection<BasketItem> basketItems)
        {
            var productsSpecification = new ProductsSpecification(basketItems.Select(b => b.ProductId).ToArray());
            var products = await _productRepository.ListAsync(productsSpecification);

            var items = basketItems.Select(basketItem =>
            {
                var product = products.First(c => c.Id == basketItem.ProductId);

                var basketItemViewModel = new BasketItemViewModel
                {
                    Id = basketItem.Id,
                    UnitPrice = basketItem.UnitPrice,
                    Quantity = basketItem.Quantity,
                    ProductId = basketItem.ProductId,
                    PictureUrl = _uriComposer.ComposePicUri(product.PictureUri),
                    ProductName = product.Name
                };
                return basketItemViewModel;
            }).ToList();

            return items;
        }

        public async Task<BasketViewModel> Map(Basket basket)
        {
            return new BasketViewModel()
            {
                BuyerId = basket.BuyerId,
                Id = basket.Id,
                Items = await GetBasketItems(basket.Items)
            };
        }
    }
}
