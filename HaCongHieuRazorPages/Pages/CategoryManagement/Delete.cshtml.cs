using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;

namespace HaCongHieuRazorPages.Pages.CategoryManagement
{
    public class DeleteModel : PageModel
    {
        private readonly ICategoryService iCategoryService;

        public DeleteModel()
        {
            iCategoryService = new CategoryService();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = iCategoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }
            else
            {
                Category = category;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(short id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = iCategoryService.GetCategoryById(id);
            if (category != null)
            {
                Category = category;
                iCategoryService.DeleteCategory(category);
            }

            return RedirectToPage("./Index");
        }
    }
}
