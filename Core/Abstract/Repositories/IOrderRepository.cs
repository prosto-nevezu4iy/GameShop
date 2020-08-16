using Core.Domain;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstract.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        bool isValid(int orderId, string email);
        int getOrdersCount(string email);
        IEnumerable<Order> GetAllWithDetails(string email);
    }
}
