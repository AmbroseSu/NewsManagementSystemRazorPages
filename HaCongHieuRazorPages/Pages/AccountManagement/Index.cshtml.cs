using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;
using System.Drawing.Printing;

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
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5; // 5 tags per page
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                return RedirectToPage("/NewsArticleManagement/Index");
            }
            var accounts = iSystemAccountService.GetAccounts();
            var accountsQuery = accounts.AsQueryable();

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

            //SystemAccount = accounts;
            TotalPages = (int)Math.Ceiling(accountsQuery.Count() / (double)PageSize);

            var currentPageString = Request.Query["currentPage"];
            if (!int.TryParse(currentPageString, out int currentPage))
            {
                currentPage = 1;
            }
            CurrentPage = Math.Clamp(currentPage, 1, TotalPages);
            SystemAccount = accountsQuery.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            return Page();
        }

        public bool IsSelected(string category)
        {
            return string.Equals(SearchAccount, category, StringComparison.OrdinalIgnoreCase);
        }

    }
}
