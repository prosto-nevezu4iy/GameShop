using Core.Domain;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstract.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetAllWithSubCategories();
        Category GetByAlias(string alias);
        Category GetCategoryWithSubCategories(string alias);
        IEnumerable<Category> GetSelectList();
        void RemoveImage(Category category);
    }
}
