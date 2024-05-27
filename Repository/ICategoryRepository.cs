using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICategoryRepository
    {
        void SaveCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        List<Category> GetCategories();
        Category GetCategoryById(short id);
        List<Category> GetCategoryByName(string name);
        List<Category> GetCategoryByDescription(string description);
    }
}
