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
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return Page();
            }

        }
    }
}
