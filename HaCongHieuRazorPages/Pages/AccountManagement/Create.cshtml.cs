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
        public string MessageError { get; set; } = string.Empty;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                return RedirectToPage("/NewsArticleManagement/Index");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var existingAccount = iSystemAccountService.GetAccountByEmail(SystemAccount.AccountEmail);
                var existingId = iSystemAccountService.GetAccountById(SystemAccount.AccountId);
                if (existingAccount != null)
                {
                    MessageError = "Email already exists in the database.";
                    return Page();
                }
                if (existingId != null)
                {
                    MessageError = "Id already exists in the database.";
                    return Page();
                }

                iSystemAccountService.SaveAccount(SystemAccount);
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
