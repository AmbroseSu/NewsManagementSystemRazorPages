using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public void SaveCategory(Category category) => CategoryDAO.SaveCategory(category);
        public void UpdateCategory(Category category) => CategoryDAO.UpdateCategory(category);
        public void DeleteCategory(Category category) => CategoryDAO.DeleteCategory(category);
        public List<Category> GetCategories() => CategoryDAO.GetCategories();
        public Category GetCategoryById(short id) => CategoryDAO.GetCategoryById(id);
        public List<Category> GetCategoryByName(string name) => CategoryDAO.GetCategoryByName(name);
        public List<Category> GetCategoryByDescription(string description) => CategoryDAO.GetCategoryByDescription(description);
    }
}
