﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;

namespace HaCongHieuRazorPages.Pages.CategoryManagement
{
    public class EditModel : PageModel
    {
        private readonly ICategoryService iCategoryService;

        public EditModel()
        {
            iCategoryService = new CategoryService();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public string MessageError { get; set; } = string.Empty;

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

            var category =  iCategoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            Category = category;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
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

            

            try
            {
                var categoryName = iCategoryService.GetCategories().Where(c => c.CategoryName.Equals(Category.CategoryName)).ToList();
                if (categoryName.Count != 0)
                {
                    MessageError = "Category Name already exists in the database.";
                    return Page();
                }
                iCategoryService.UpdateCategory(Category);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!CategoryExists(Category.CategoryId))
                {
                    return NotFound();
                }
                else
                {
                    MessageError = ex.Message;
                    return Page();
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CategoryExists(short id)
        {
            Category = iCategoryService.GetCategoryById(id);
            if(Category == null)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }
    }
}
