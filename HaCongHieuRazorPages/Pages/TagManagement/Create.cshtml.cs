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
            return Page();
        }

        [BindProperty]
        public Tag Tag { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            iTagService.SaveTag(Tag);

            return RedirectToPage("./Index");
        }
    }
}
