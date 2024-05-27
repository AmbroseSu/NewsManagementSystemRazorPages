using BusinessObject;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository iCategoryRepository;

        public CategoryService()
        {
            iCategoryRepository = new CategoryRepository();
        }

        public void SaveCategory(Category category)
        {
            iCategoryRepository.SaveCategory(category);
        }
        public void UpdateCategory(Category category)
        {
            iCategoryRepository.UpdateCategory(category);
        }
        public void DeleteCategory(Category category)
        {
            iCategoryRepository.DeleteCategory(category);
        }
        public List<Category> GetCategories()
        {
            return iCategoryRepository.GetCategories();
        }
        public Category GetCategoryById(short id)
        {
            return iCategoryRepository.GetCategoryById(id);
        }

        public List<Category> GetCategoryByName(string name)
        {
            return iCategoryRepository.GetCategoryByName(name);
        }

        public List<Category> GetCategoryByDescription(string description)
        {
            return iCategoryRepository.GetCategoryByDescription(description);
        }
    }
}
