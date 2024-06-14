using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;
using DataAccess;

namespace HaCongHieuRazorPages.Pages.NewsArticleManagement
{
    public class EditModel : PageModel
    {
        private readonly INewsArticleService iNewsArticleService;
        private readonly ICategoryService iCategoryService;
        private readonly ISystemAccountService iSystemAccountService;
        private readonly ITagService iTagService;

        public EditModel()
        {
            iNewsArticleService = new NewsArticleService();
            iCategoryService = new CategoryService();
            iSystemAccountService = new SystemAccountService();
            iTagService = new TagService();
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; } = default!;

        [BindProperty]
        public List<int> SelectedTagIds { get; set; } = new List<int>();

        public List<Tag> AvailableTags { get; set; }

        public IActionResult OnGet(string id)
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

            var newsArticle = iNewsArticleService.GetNewsArticleById(id);
            if (newsArticle == null)
            {
               return NotFound();
            }
            NewsArticle = newsArticle;

            // Lấy danh sách các tag đã chọn
            SelectedTagIds = iNewsArticleService.GetTagsByNewsArticleId(id);

            ViewData["CategoryId"] = new SelectList(iCategoryService.GetCategories(), "CategoryId", "CategoryName", NewsArticle.CategoryId);
            ViewData["CreatedById"] = new SelectList(iSystemAccountService.GetAccounts(), "AccountId", "AccountId", NewsArticle.CreatedById);

            // Lấy danh sách các tag có sẵn
            AvailableTags = iTagService.GetTags();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Staff")
            {
                return RedirectToPage("/NewsArticleManagement/Index");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                NewsArticle.ModifiedDate = DateTime.Now;
                // Lấy danh sách các tag đã chọn
                var selectedTags = iTagService.GetTagsByIds(SelectedTagIds);

                // Cập nhật danh sách các tag cho bài viết
                NewsArticle.Tags = selectedTags;

                // Cập nhật bài viết
                iNewsArticleService.UpdateNewsArticle(NewsArticle);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsArticleExists(NewsArticle.NewsArticleId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

    private bool NewsArticleExists(string id)
        {
            NewsArticle = iNewsArticleService.GetNewsArticleById(id);
            if(NewsArticle == null)
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
