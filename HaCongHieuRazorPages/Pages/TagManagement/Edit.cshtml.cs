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

        public async Task<IActionResult> OnGetAsync(int id)
        {
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            

            try
            {
                iTagService.UpdateTag(Tag);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(Tag.TagId))
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
