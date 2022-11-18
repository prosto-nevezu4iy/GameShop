using Microsoft.AspNetCore.Mvc.Rendering;
using Web.ViewModels;

namespace Web.Interfaces
{
    public interface IOrderViewModelService
    {
        Task<IEnumerable<OrderViewModel>> GetOrders(Guid buyerId);
        Task<OrderViewModel> GetOrderDetails(int orderId);
    }
}
