using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Queries
{
    public class BasketQueryService : IBasketQueryService
    {
        private readonly ApplicationDbContext _dbContext;

        public BasketQueryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CountTotalBasketItems(Guid userId)
        {
            var totalItems = await _dbContext.Baskets
                .Where(basket => basket.BuyerId == userId)
                .SelectMany(item => item.Items)
                .CountAsync();

            return totalItems;
        }
    }
}
