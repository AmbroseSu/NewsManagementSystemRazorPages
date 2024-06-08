using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using Service;

namespace HaCongHieuRazorPages.Pages.NewsArticleManagement
{
    public class CreateModel : PageModel
    {
        private readonly INewsArticleService iNewsArticleService;
        private readonly ICategoryService iCategoryService;
        private readonly ISystemAccountService iSystemAccountService;
        private readonly ITagService iTagService;


        public CreateModel()
        {
            iNewsArticleService = new NewsArticleService();
            iCategoryService = new CategoryService();
            iSystemAccountService = new SystemAccountService();
            iTagService = new TagService();
        }

        [BindProperty]
        public NewsArticle NewsArticle { get; set; }

        [BindProperty]
        public List<int> SelectedTagIds { get; set; } = new List<int>();

        public List<Tag> AvailableTags { get; set; }

        public IActionResult OnGet()
        {
            ViewData["CategoryId"] = new SelectList(iCategoryService.GetCategories(), "CategoryId", "CategoryName");
            ViewData["CreatedById"] = new SelectList(iSystemAccountService.GetAccounts(), "AccountId", "AccountId");
            AvailableTags = iTagService.GetTags();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Đặt ngày giờ hiện tại cho CreatedDate và ModifiedDate
            NewsArticle.CreatedDate = DateTime.Now;
            NewsArticle.ModifiedDate = DateTime.Now;

            // Lấy danh sách các tag đã chọn
            var selectedTags = iTagService.GetTagsByIds(SelectedTagIds);

            // Cập nhật danh sách các tag cho bài viết
            NewsArticle.Tags = selectedTags;

            // Lưu bài viết
            iNewsArticleService.SaveNewsArticle(NewsArticle);

            return RedirectToPage("./Index");
        }
    }
}