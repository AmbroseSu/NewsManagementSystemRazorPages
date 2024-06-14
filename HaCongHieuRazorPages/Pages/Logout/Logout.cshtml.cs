using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HaCongHieuRazorPages.Pages.Logout
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.Remove("UserEmail");
            HttpContext.Session.Remove("UserRole");

            return RedirectToPage("/Login/Login");
        }
    }
}
