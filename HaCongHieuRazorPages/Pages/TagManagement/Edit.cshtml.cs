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

namespace HaCongHieuRazorPages.Pages.TagManagement
{
    public class EditModel : PageModel
    {
        private readonly ITagService iTagService;

        public EditModel()
        {
            iTagService = new TagService();
        }

        [BindProperty]
        public Tag Tag { get; set; } = default!;

        public string MessageError { get; set; } = string.Empty;

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

            var tag =  iTagService.GetTagById(id);
            if (tag == null)
            {
                return NotFound();
            }
            Tag = tag;
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
                var checkName = iTagService.GetTags().Where(t => t.TagName.Equals(Tag.TagName)).ToList();
                if (checkName.Count != 0)
                {
                    MessageError = "Tag Name already exists in the database.";
                    return Page();
                }
                iTagService.UpdateTag(Tag);
                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException ex )
            {
                if (!TagExists(Tag.TagId))
                {
                    return NotFound();
                }
                else
                {
                    MessageError = ex.Message;
                    throw;
                }
            }

            
        }

        private bool TagExists(int id)
        {
            Tag = iTagService.GetTagById(id);
            if(Tag == null)
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
