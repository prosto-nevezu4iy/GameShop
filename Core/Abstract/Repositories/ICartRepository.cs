using Core.Domain;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Abstract.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        string CartSessionKey { get; }
        Cart GetCart(int id);
        string GetCartId(HttpContextBase context);
        void AddToCart(Product product, string cartId);
        int IncreaseItem(int id, string cartId);
        int DecreaseItem(int id, string cartId);
        void RemoveFromCart(int id, string cartId);
        void EmptyCart(string cartId);
        IEnumerable<Cart> GetCartItems(string cartId);
        int GetCount(string cartId);
        decimal GetTotal(string cartId);
        void MigrateCart(string userId, string cartId);
        int CreateOrder(Order order, string cartId);
        int getCartCount(string email);
    }
}
