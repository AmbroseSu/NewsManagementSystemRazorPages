using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;

namespace HaCongHieuRazorPages.Pages.CategoryManagement
{
    public class CategoryViewModel : PageModel
    {
        private ICategoryService iCategoryService;
        public List<Category> categories { get; set; } = new List<Category>();
        public string ErrorMessage { get; set; }

        public CategoryViewModel()
        {
            iCategoryService = new CategoryService();
        }

        public void OnGet()
        {
            try
            {
                categories = iCategoryService.GetCategories();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred: {ex.Message}";
                // return Page();
            }
             
        }
    }
}
