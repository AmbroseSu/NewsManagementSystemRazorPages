using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using Service;

namespace HaCongHieuRazorPages.Pages.CategoryManagement
{
    public class CreateModel : PageModel
    {
        /*        private readonly BusinessObject.FunewsManagementDbContext _context;

                public CreateModel(BusinessObject.FunewsManagementDbContext context)
                {
                    _context = context;
                }*/

        private readonly ICategoryService iCategoryService;

        public CreateModel()
        {
            iCategoryService = new CategoryService();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            /*_context.Categories.Add(Category);
            await _context.SaveChangesAsync();*/
            iCategoryService.SaveCategory(Category);


            return RedirectToPage("./Index");
        }
    }
}         
