using Core.Abstract.Repositories;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;

namespace Core.Infrastructure.Repositories
{
    public class WishListRepository : Repository<WishList>, IWishListRepository
    {
        public WishListRepository(GameShopContext context)
            : base(context)
        {

        }

        public GameShopContext GameShopContext
        {
            get { return Context as GameShopContext; }
        }

        public string WishListSessionKey
        {
            get { return "UserId"; }
        }

        public WishList GetWishList(int id)
        {
            return GameShopContext.WishLists
                        .Include(c => c.Product)
                        .FirstOrDefault(c => c.Id == id);
        }

        public string GetWishListId(HttpContextBase context)
        {
            if (context.Session[WishListSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[WishListSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[WishListSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[WishListSessionKey].ToString();
        }

        public bool isProductAtWishList(int productId, string wishListId)
        {
            return GameShopContext.WishLists.Any(
                w => w.WishListId == wishListId 
                && w.ProductId == productId);
        }

        public void AddToWishList(Product product, string wishListId)
        {
            // Get the matching cart and album instances
            var wishListItem = GameShopContext.WishLists.SingleOrDefault(
                w => w.WishListId == wishListId
                && w.ProductId == product.Id);

            if (wishListItem == null)
            {
                // Create a new cart item if no cart item exists
                wishListItem = new WishList
                {
                    ProductId = product.Id,
                    WishListId = wishListId
                };
                GameShopContext.WishLists.Add(wishListItem);
            }
        }

        public void RemoveFromWishList(int id, string wishListId)
        {
            // Get the cart
            var wishListItem = GameShopContext.WishLists.Single(
                w => w.WishListId == wishListId
                && w.ProductId == id);

            if (wishListItem != null)
            {
                GameShopContext.WishLists.Remove(wishListItem);
            }
        }

        public IEnumerable<WishList> GetWishListItems(string wishListId)
        {
            return GameShopContext.WishLists
                    .Where(w => w.WishListId == wishListId)
                    .Include(w => w.Product.Category.Parent)
                    .Include(w => w.Product.Images);
        }

        public void CreateCart(int productId, string wishListId)
        {
            var cartItem = GameShopContext.Carts.SingleOrDefault(
                w => w.CartId == wishListId
                && w.ProductId == productId);

            if(cartItem == null)
            {
                var cart = new Cart
                {
                    CartId = wishListId,
                    ProductId = productId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                GameShopContext.Carts.Add(cart);
            } else
            {
                cartItem.Count++;
            }

            RemoveFromWishList(productId, wishListId);
        }

        public void MigrateWishList(string userId, string wishListId)
        {
            var wishList = GameShopContext.WishLists.Where(
                c => c.WishListId == wishListId);

            foreach (WishList item in wishList)
            {
                item.WishListId = userId;
            }
        }

        public int getWishListCount(string email)
        {
            return GameShopContext.WishLists.Count(w => w.WishListId == email);
        }
    }
}
