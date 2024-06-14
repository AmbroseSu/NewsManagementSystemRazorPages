using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;

namespace HaCongHieuRazorPages.Pages.TagManagement
{
    public class DetailsModel : PageModel
    {
        private readonly ITagService iTagService;

        public DetailsModel()
        {
            iTagService = new TagService();
        }

        public Tag Tag { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
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

            var tag = iTagService.GetTagById(id);
            if (tag == null)
            {
                return NotFound();
            }
            else
            {
                Tag = tag;
            }
            return Page();
        }
    }
}
