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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(GameShopContext context)
            : base(context)
        {

        }

        public GameShopContext GameShopContext
        {
            get { return Context as GameShopContext; }
        }

        public Category GetByAlias(string alias)
        {
            return GameShopContext.Categories
                .Include(c => c.Parent)
                .FirstOrDefault(c => c.Alias == alias);
        }

        public IEnumerable<Category> GetAllWithSubCategories()
        {
            return GameShopContext.Categories
                        .Include(c => c.Parent)
                        .Include(c => c.Children)
                        .ToList();
        }

        public Category GetCategoryWithSubCategories(string alias)
        {
            return GameShopContext.Categories
                .Include(c => c.Children)
                .FirstOrDefault(c => c.Alias == alias);
        }

        public IEnumerable<Category> GetSelectList()
        {
            return GameShopContext.Categories
                .Select(c => new Category{ Id = c.Id, Name = c.Name });
        }

        public void RemoveImage(Category category)
        {
            category.Image = null;
            category.ImageMimeType = null;
        }
    }
}
