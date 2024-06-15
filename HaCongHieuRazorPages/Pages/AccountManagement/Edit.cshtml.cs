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

namespace HaCongHieuRazorPages.Pages.AccountManagement
{
    public class EditModel : PageModel
    {
        private readonly ISystemAccountService iSystemAccountService;

        public EditModel()
        {
            iSystemAccountService = new SystemAccountService();
        }

        [BindProperty]
        public SystemAccount SystemAccount { get; set; } = default!;

        public string MessageError { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(short id)
        {
            var email = HttpContext.Session.GetString("UserEmail");
            var role = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(role) )
            {
                return RedirectToPage("/Login/Login");
            }
            if (id == null)
            {
                return NotFound();
            }
            if (role == "Staff" || role == "Lecturer")
            {
                var systemaccountt = iSystemAccountService.GetAccountByEmail(email);
                if (systemaccountt == null)
                {
                    return NotFound();
                }
                SystemAccount = systemaccountt;
                return Page();
            }
            else
            {
                var systemaccount = iSystemAccountService.GetAccountById(id);
                if (systemaccount == null)
                {
                    return NotFound();
                }
                SystemAccount = systemaccount;
                return Page();
            }

                
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
                var existingAccount = iSystemAccountService.GetAccountByEmail(SystemAccount.AccountEmail);
                if (existingAccount != null)
                {
                    MessageError = "Email already exists in the database.";
                    return Page();
                }
                var accountRole = iSystemAccountService.GetAccountById(SystemAccount.AccountId);
                SystemAccount.AccountRole = accountRole.AccountRole;
                iSystemAccountService.UpdateAccount(SystemAccount);
            }
            catch (DbUpdateConcurrencyException Db)
            {
                if (!SystemAccountExists(SystemAccount.AccountId))
                {
                    return NotFound();
                }
                else
                {
                    MessageError = Db.Message;
                    return Page();
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SystemAccountExists(short id)
        {
                SystemAccount = iSystemAccountService.GetAccountById(id);
            if(SystemAccount == null)
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
