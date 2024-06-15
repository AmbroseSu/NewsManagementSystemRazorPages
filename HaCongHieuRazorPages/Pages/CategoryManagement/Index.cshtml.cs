using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;
using System.Drawing.Printing;

namespace HaCongHieuRazorPages.Pages.CategoryManagement
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryService iCategoryService;

        public IndexModel()
        {
            iCategoryService = new CategoryService();
        }

        public IList<Category> Category { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchInput { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchCategory { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5; // 5 tags per page
        public int TotalPages { get; set; }

        /*public async Task OnGetAsync()
        {
            Category = iCategoryService.GetCategories();
        }*/
        public async Task<IActionResult> OnGetAsync()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(role) || role != "Staff")
            {
                return RedirectToPage("/NewsArticleManagement/Index");
            }
            var categories = iCategoryService.GetCategories();
            var categoriesQuery = categories.AsQueryable();

            if (!string.IsNullOrEmpty(SearchInput) && !string.IsNullOrEmpty(SearchCategory))
            {
                categories = SearchCategory switch
                {
                    "CategoryId" => categories.Where(c => c.CategoryId.ToString().Contains(SearchInput)).ToList(),
                    "CategoryName" => iCategoryService.GetCategoryByName(SearchInput),
                    "CategoryDesciption" => iCategoryService.GetCategoryByDescription(SearchInput),
                    _ => categories
                };
            }

            //Category = categories;
            TotalPages = (int)Math.Ceiling(categoriesQuery.Count() / (double)PageSize);

            var currentPageString = Request.Query["currentPage"];
            if (!int.TryParse(currentPageString, out int currentPage))
            {
                currentPage = 1;
            }
            CurrentPage = Math.Clamp(currentPage, 1, TotalPages);
            Category = categoriesQuery.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            return Page();
        }

        public bool IsSelected(string category)
        {
            return string.Equals(SearchCategory, category, StringComparison.OrdinalIgnoreCase);
        }

    }
}
