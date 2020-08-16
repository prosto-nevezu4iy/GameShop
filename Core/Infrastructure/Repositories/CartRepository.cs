using Core.Abstract.Repositories;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Infrastructure.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(GameShopContext context)
            : base(context)
        {

        }

        public GameShopContext GameShopContext
        {
            get { return Context as GameShopContext; }
        }

        public string CartSessionKey
        {
            get { return "UserId"; }
        }

        public Cart GetCart(int id)
        {
            return GameShopContext.Carts
                        .Include(c => c.Product)
                        .FirstOrDefault(c => c.Id == id);
        }

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        public void AddToCart(Product product, string cartId)
        {
            // Get the matching cart and album instances
            var cartItem = GameShopContext.Carts.SingleOrDefault(
                c => c.CartId == cartId
                && c.ProductId == product.Id);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    ProductId = product.Id,
                    CartId = cartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                GameShopContext.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.Count++;
            }
        }

        public int IncreaseItem(int id, string cartId)
        {
            // Get the cart
            var cartItem = GameShopContext.Carts.Single(
                cart => cart.CartId == cartId
                && cart.Id == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                cartItem.Count++;
                itemCount = cartItem.Count;
            }
            return itemCount;
        }

        public int DecreaseItem(int id, string cartId)
        {
            // Get the cart
            var cartItem = GameShopContext.Carts.Single(
                cart => cart.CartId == cartId
                && cart.Id == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    GameShopContext.Carts.Remove(cartItem);
                }
            }
            return itemCount;
        }

        public void RemoveFromCart(int id, string cartId)
        {
            // Get the cart
            var cartItem = GameShopContext.Carts.Single(
                cart => cart.CartId == cartId
                && cart.Id == id);

            if (cartItem != null)
            {
                GameShopContext.Carts.Remove(cartItem);
            }
        }

        public IEnumerable<Cart> GetCartItems(string cartId)
        {
            return GameShopContext.Carts
                    .Where(c => c.CartId == cartId)
                    .Include(c => c.Product.Category.Parent)
                    .Include(w => w.Product.Images);
        }

        public int GetCount(string cartId)
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in GameShopContext.Carts
                          where cartItems.CartId == cartId
                          select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal(string cartId)
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in GameShopContext.Carts
                              where cartItems.CartId == cartId
                              select (int?)cartItems.Count *
                              cartItems.Product.Price).Sum();

            return total ?? decimal.Zero;
        }

        public void MigrateCart(string userId, string cartId)
        {
            var shoppingCart = GameShopContext.Carts.Where(
                c => c.CartId == cartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userId;
            }
        }

        public void EmptyCart(string cartId)
        {
            var cartItems = GameShopContext.Carts.Where(
                cart => cart.CartId == cartId);

            foreach (var cartItem in cartItems)
            {
                GameShopContext.Carts.Remove(cartItem);
            }
        }

        public int CreateOrder(Order order, string cartId)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems(cartId);
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    OrderId = order.Id,
                    UnitPrice = item.Product.Price,
                    Quantity = item.Count
                };
                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.Product.Price);

                GameShopContext.OrderDetails.Add(orderDetail);
            }
            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            // Empty the shopping cart
            EmptyCart(cartId);
            // Return the OrderId as the confirmation number
            return order.Id;
        }

        public int getCartCount(string email)
        {
            return GameShopContext.Carts.Count(c => c.CartId == email);
        }
    }
}
