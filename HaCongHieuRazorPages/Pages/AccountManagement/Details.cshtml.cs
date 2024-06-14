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
    public class DetailsModel : PageModel
    {
        private readonly ISystemAccountService iSystemAccountService;

        public DetailsModel()
        {
            iSystemAccountService = new SystemAccountService();
        }

        public SystemAccount SystemAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short id)
        {
            var email = HttpContext.Session.GetString("UserEmail");
            var role = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(role))
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
                else
                {
                    SystemAccount = systemaccountt;
                }
                return Page();
            }
            else
            {
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

                
        }
    }
}
