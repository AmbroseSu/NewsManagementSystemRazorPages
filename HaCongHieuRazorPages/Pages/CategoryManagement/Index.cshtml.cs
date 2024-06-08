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

        /*public async Task OnGetAsync()
        {
            Category = iCategoryService.GetCategories();
        }*/
        public async Task OnGetAsync()
        {
            var categories = iCategoryService.GetCategories();

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

            Category = categories;
        }

        public bool IsSelected(string category)
        {
            return string.Equals(SearchCategory, category, StringComparison.OrdinalIgnoreCase);
        }

    }
}
