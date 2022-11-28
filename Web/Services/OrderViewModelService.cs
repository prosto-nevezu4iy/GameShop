using ApplicationCore.Entities.OrderAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class OrderViewModelService : IOrderViewModelService
    {
        private readonly IReadRepository<Order> _orderRepository;

        public OrderViewModelService(IReadRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderViewModel> GetOrderDetails(int orderId)
        {
            var spec = new OrderWithItemsByIdSpecification(orderId);
            var order = await _orderRepository.FirstOrDefaultAsync(spec);

            if (order == null)
            {
                return default;
            }

            return new OrderViewModel
            {
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems.Select(oi => new OrderItemViewModel
                {
                    PictureUrl = oi.ProductOrdered.PictureUri,
                    ProductId = oi.ProductOrdered.ProductId,
                    ProductName = oi.ProductOrdered.ProductName,
                    UnitPrice = oi.UnitPrice,
                    Units = oi.Units
                }).ToList(),
                OrderNumber = order.Id,
                ShippingAddress = order.ShipToAddress,
                Total = order.Total()
            };
        }

        public async Task<IEnumerable<OrderViewModel>> GetOrders(Guid buyerId)
        {
            var specification = new OrdersWithItemsSpecification(buyerId);
            var orders = await _orderRepository.ListAsync(specification);

            return orders.Select(o => new OrderViewModel
            {
                OrderDate = o.OrderDate,
                OrderItems = o.OrderItems.Select(oi => new OrderItemViewModel()
                {
                    PictureUrl = oi.ProductOrdered.PictureUri,
                    ProductId = oi.ProductOrdered.ProductId,
                    ProductName = oi.ProductOrdered.ProductName,
                    UnitPrice = oi.UnitPrice,
                    Units = oi.Units
                }).ToList(),
                OrderNumber = o.Id,
                ShippingAddress = o.ShipToAddress,
                Total = o.Total()
            });
        }
    }
}
