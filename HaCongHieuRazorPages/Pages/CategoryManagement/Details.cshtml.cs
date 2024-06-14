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
    public class DetailsModel : PageModel
    {
        private readonly ICategoryService iCategoryService;

        public DetailsModel()
        {
            iCategoryService = new CategoryService();
        }

        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short id)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(role) || role != "Staff")
            {
                return RedirectToPage("/NewsArticleManagement/Index");
            }
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
    }
}
