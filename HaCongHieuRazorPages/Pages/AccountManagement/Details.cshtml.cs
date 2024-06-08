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
    }
}
