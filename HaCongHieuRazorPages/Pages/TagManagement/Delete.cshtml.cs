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
    public class DeleteModel : PageModel
    {
        private readonly ITagService iTagService;

        public DeleteModel()
        {
            iTagService = new TagService();
        }

        [BindProperty]
        public Tag Tag { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = iTagService.GetTagById(id);
            if (tag != null)
            {
                
                iTagService.DeleteTag(tag);
            }

            return RedirectToPage("./Index");
        }
    }
}
