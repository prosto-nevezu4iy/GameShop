using Core.Abstract.Repositories;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Core.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(GameShopContext context)
            : base(context)
        {

        }

        public GameShopContext GameShopContext
        {
            get { return Context as GameShopContext; }
        }

        public IEnumerable<Order> GetAllWithDetails(string email)
        {
            return GameShopContext.Orders
                .Include(o => o.OrderDetails.Select(od => od.Product.Images))
                .Include(o => o.OrderDetails.Select(od => od.Product.Category.Parent))
                .Where(o => o.Email == email);
        }

        public int getOrdersCount(string email)
        {
            return GameShopContext.Orders.Count(o => o.Email == email);
        }

        public bool isValid(int orderId, string email)
        {
            return GameShopContext.Orders.Any(o => o.Id == orderId && o.Email == email);
        }
    }
}
