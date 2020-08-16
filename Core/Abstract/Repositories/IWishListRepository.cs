using Core.Domain;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Abstract.Repositories
{
    public interface IWishListRepository : IRepository<WishList>
    {
        string WishListSessionKey { get; }
        WishList GetWishList(int id);
        string GetWishListId(HttpContextBase context);
        bool isProductAtWishList(int productId, string wishListId);
        void AddToWishList(Product product, string wishListId);
        void RemoveFromWishList(int id, string wishListId);
        IEnumerable<WishList> GetWishListItems(string wishListId);
        void MigrateWishList(string userId, string wishListId);
        void CreateCart(int productId, string wishListId);
        int getWishListCount(string email);
    }
}
