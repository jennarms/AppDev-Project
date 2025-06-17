using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MetroLayag.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace MetroLayag.Pages
{
    public class EditAccountModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditAccountModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
                return RedirectToPage("/Login");

            var role = HttpContext.Session.GetString("Role");
            if (role != "MainAdmin")
                return RedirectToPage("/AccessDenied");

            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                Username = user.Username;
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
                return RedirectToPage("/Login");

            var role = HttpContext.Session.GetString("Role");
            if (role != "MainAdmin")
                return RedirectToPage("/AccessDenied");

            var userId = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                if (!string.IsNullOrWhiteSpace(Username))
                {
                    user.Username = Username;
                    HttpContext.Session.SetString("Username", Username);
                }

                if (!string.IsNullOrWhiteSpace(Password))
                {
                    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password);
                }

                _context.SaveChanges();
                SuccessMessage = "Account updated successfully.";
            }
            else
            {
                ErrorMessage = "User not found.";
            }

            return Page();
        }
    }
}
