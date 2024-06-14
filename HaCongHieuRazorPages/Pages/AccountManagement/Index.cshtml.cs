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
    public class IndexModel : PageModel
    {
        private readonly ISystemAccountService iSystemAccountService;

        public IndexModel()
        {
            iSystemAccountService = new SystemAccountService();
        }

        public IList<SystemAccount> SystemAccount { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchInput { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchAccount { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                return RedirectToPage("/NewsArticleManagement/Index");
            }
            var accounts = iSystemAccountService.GetAccounts();

            if (!string.IsNullOrEmpty(SearchInput) && !string.IsNullOrEmpty(SearchAccount))
            {
                accounts = SearchAccount switch
                {
                    "AccountId" => accounts.Where(c => c.AccountId.ToString().Contains(SearchInput)).ToList(),
                    "AccountName" => iSystemAccountService.GetSystemAccountsByName(SearchInput),
                    "AccountEmail" => iSystemAccountService.GetSystemAccountsByEmail(SearchInput),
                    "AccountRole" => iSystemAccountService.GetSystemAccountsByRole(Int32.Parse(SearchInput)),
                    _ => accounts
                };
            }

            SystemAccount = accounts;
            return Page();
        }

        public bool IsSelected(string category)
        {
            return string.Equals(SearchAccount, category, StringComparison.OrdinalIgnoreCase);
        }

    }
}
