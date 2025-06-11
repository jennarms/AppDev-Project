using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace MetroLayag.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnPost()
        {
            // Hardcoded admin credentials
            if (Username == "admin" && Password == "pass123")
            {
                HttpContext.Session.SetString("IsLoggedIn", "true");
                return RedirectToPage("/Index");
            }

            ErrorMessage = "Invalid username or password";
            return Page();
        }
    }
}
