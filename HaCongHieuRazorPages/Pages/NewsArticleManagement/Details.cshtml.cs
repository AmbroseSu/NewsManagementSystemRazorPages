using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HaCongHieuRazorPages.Pages.NewsArticleManagement
{
    public class DetailsModel : PageModel
    {
        private readonly INewsArticleService iNewsArticleService;
        private readonly ICategoryService iCategoryService;
        private readonly ISystemAccountService iSystemAccountService;

        public DetailsModel()
        {
            iNewsArticleService = new NewsArticleService();
            iCategoryService = new CategoryService();
            iSystemAccountService = new SystemAccountService();
        }

        public NewsArticle NewsArticle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Staff")
            {
                return RedirectToPage("/NewsArticleManagement/Index");
            }

            if (id == null)
            {
                return NotFound();
            }

            var newsarticle = iNewsArticleService.GetNewsArticleById(id);
            if (newsarticle == null)
            {
                return NotFound();
            }
            else
            {
                NewsArticle = newsarticle;
            }
            return Page();
        }
    }
}
