using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;

namespace HaCongHieuRazorPages.Pages.NewsArticleManagement
{
    public class DeleteModel : PageModel
    {
        private readonly INewsArticleService iNewsArticleService;

        public DeleteModel()
        {
            iNewsArticleService = new NewsArticleService();
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(string id)
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
            if (newsarticle != null)
            {
                iNewsArticleService.DeleteNewsArticle(newsarticle);
            }

            return RedirectToPage("./Index");
        }
    }
}
