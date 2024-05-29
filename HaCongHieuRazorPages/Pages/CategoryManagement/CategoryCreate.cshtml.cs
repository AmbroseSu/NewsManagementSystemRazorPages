using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;

namespace HaCongHieuRazorPages.Pages.CategoryManagement
{
    public class CategoryCreateModel : PageModel
    {
        private readonly ICategoryService iCategoryService;

        [BindProperty]
        public Category category { get; set; }

        //public bool IsSuccess { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public CategoryCreateModel()
        {
            iCategoryService = new CategoryService();
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            try
            {
                iCategoryService.SaveCategory(category);
                SuccessMessage = "Category created successfully!";
                category = new Category();
                ModelState.Clear(); // Clear the form
                return Page();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred: {ex.Message}";
                return Page();
            }

        }
    }
}
