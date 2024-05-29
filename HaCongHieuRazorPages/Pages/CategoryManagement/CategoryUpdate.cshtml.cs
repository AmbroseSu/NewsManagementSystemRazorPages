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
        public short categoryId { get; set; }
        public Category category { get; set; }
        public void OnGet()
        {
            category = iCategoryService.GetCategoryById(categoryId);
        }
    }
}
