using ApplicationCore.Entities.BasketAggregate;
using Web.ViewModels;

namespace Web.Interfaces
{
    public interface IBasketViewModelService
    {
        Task<BasketViewModel> GetOrCreateBasketForUser(Guid userId);
        Task<int> CountTotalBasketItems(Guid userId);
        Task<BasketViewModel> Map(Basket basket);
    }
}
