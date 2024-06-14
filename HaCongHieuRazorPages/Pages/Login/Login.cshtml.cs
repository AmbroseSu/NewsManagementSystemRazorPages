using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject;
using Repository;
using Service;

namespace HaCongHieuRazorPages.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly ISystemAccountService iSystemAccountService;
        private string defaultEmail;
        private string defaultPassword;

        public LoginModel()
        {
            iSystemAccountService = new SystemAccountService();
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            defaultEmail = configuration.GetSection("DefaultAccount")["Email"];
            defaultPassword = configuration.GetSection("DefaultAccount")["Password"];
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

            if (SystemAccount.AccountEmail.Equals(defaultEmail) && SystemAccount.AccountPassword.Equals(defaultPassword))
            {
                HttpContext.Session.SetString("UserEmail", defaultEmail);
                HttpContext.Session.SetString("UserRole", "Admin");
                return RedirectToPage("/AccountManagement/Index");
            }
            else
            {
                SystemAccount systemAccount = iSystemAccountService.GetAccountByEmail(SystemAccount.AccountEmail);
                if (systemAccount != null && systemAccount.AccountPassword.Equals(SystemAccount.AccountPassword) && systemAccount.AccountRole == 1)
                {
                    HttpContext.Session.SetString("UserEmail", SystemAccount.AccountEmail);
                    HttpContext.Session.SetString("UserRole", "Staff");
                    return RedirectToPage("/NewsArticleManagement/Index");
                }
                else
                {
                    if (systemAccount != null && systemAccount.AccountPassword.Equals(SystemAccount.AccountPassword) && systemAccount.AccountRole == 2)
                    {
                        HttpContext.Session.SetString("UserEmail", SystemAccount.AccountEmail);
                        HttpContext.Session.SetString("UserRole", "Lecturer");
                        return RedirectToPage("/NewsArticleManagement/Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return Page();
                    }
                }

            }





        }
    }
}
