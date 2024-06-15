using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using Service;

namespace HaCongHieuRazorPages.Pages.TagManagement
{
    public class CreateModel : PageModel
    {
        private readonly ITagService iTagService;

        public CreateModel()
        {
            iTagService = new TagService();
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
        public Tag Tag { get; set; } = default!;
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
            try
            {
                var checkId = iTagService.GetTagById(Tag.TagId);
                var checkName = iTagService.GetTags().Where(t => t.TagName.Equals(Tag.TagName)).ToList();
                if (checkId != null)
                {
                    MessageError = "Id already exists in the database.";
                    return Page();
                }
                if (checkName.Count != 0)
                {
                    MessageError = "Tag Name already exists in the database.";
                    return Page();
                }
                iTagService.SaveTag(Tag);
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
