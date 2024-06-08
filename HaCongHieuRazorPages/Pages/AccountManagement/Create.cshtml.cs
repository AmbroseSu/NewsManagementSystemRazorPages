using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using Service;

namespace HaCongHieuRazorPages.Pages.AccountManagement
{
    public class CreateModel : PageModel
    {
        private readonly ISystemAccountService iSystemAccountService;

        public CreateModel()
        {
            iSystemAccountService = new SystemAccountService();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SystemAccount SystemAccount { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            iSystemAccountService.SaveAccount(SystemAccount);

            return RedirectToPage("./Index");
        }
    }
}
