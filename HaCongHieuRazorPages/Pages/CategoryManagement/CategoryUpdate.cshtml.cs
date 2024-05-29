using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;

namespace HaCongHieuRazorPages.Pages.CategoryManagement
{
    public class CategoryUpdateModel : PageModel
    {
        private readonly ICategoryService iCategoryService;


        [BindProperty]
        public Category category { get; set; }

        public CategoryUpdateModel()
        {
            iCategoryService = new CategoryService();
        }

        public IActionResult OnGet(short id)
        {
            try
            {
                category = iCategoryService.GetCategoryById(id);
                if (category == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return Page();
            }

            return Page();
        }
        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                iCategoryService.UpdateCategory(category);
                return RedirectToPage("/CategoryManagement/CategoryView");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return Page();
            }
        }
    }
}
