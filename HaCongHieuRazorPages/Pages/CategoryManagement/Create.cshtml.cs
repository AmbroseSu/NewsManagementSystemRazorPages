﻿using System;
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
            var role = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(role) || role != "Staff")
            {
                return RedirectToPage("/NewsArticleManagement/Index");
            }
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;
        public string MessageError { get; set; } = string.Empty;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(role) || role != "Staff")
            {
                return RedirectToPage("/NewsArticleManagement/Index");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            /*_context.Categories.Add(Category);
            await _context.SaveChangesAsync();*/

            try
            {
                var categoryName = iCategoryService.GetCategories().Where(c => c.CategoryName.Equals(Category.CategoryName)).ToList();
                if(categoryName.Count != 0)
                {
                    MessageError = "Category Name already exists in the database.";
                    return Page();
                }
                iCategoryService.SaveCategory(Category);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                MessageError = $"Error: {ex.Message}";
                return Page();
            }
        }
    }
}         
