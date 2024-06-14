using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;

namespace HaCongHieuRazorPages.Pages.AccountManagement
{
    public class DeleteModel : PageModel
    {
        private readonly ISystemAccountService iSystemAccountService;

        public DeleteModel()
        {
            iSystemAccountService = new SystemAccountService();
        }

        [BindProperty]
        public SystemAccount SystemAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short id)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                return RedirectToPage("/NewsArticleManagement/Index");
            }
            if (id == null)
            {
                return NotFound();
            }

            var systemaccount = iSystemAccountService.GetAccountById(id);

            if (systemaccount == null)
            {
                return NotFound();
            }
            else
            {
                SystemAccount = systemaccount;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(short id)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                return RedirectToPage("/NewsArticleManagement/Index");
            }
            if (id == null)
            {
                return NotFound();
            }

            var systemaccount = iSystemAccountService.GetAccountById(id);
            if (systemaccount != null)
            {
                //SystemAccount = systemaccount;
                iSystemAccountService.DeleteAccount(systemaccount);
            }

            return RedirectToPage("./Index");
        }
    }
}
