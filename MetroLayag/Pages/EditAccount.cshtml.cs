using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MetroLayag.Data;
using System.Linq;

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
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                ErrorMessage = "User not found in session.";
                return RedirectToPage("/Login");
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                ErrorMessage = "User does not exist.";
                return RedirectToPage("/Login");
            }

            Username = user.Username;
            return Page();
        }

        public IActionResult OnPost()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                ErrorMessage = "User not found in session.";
                return RedirectToPage("/Login");
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                ErrorMessage = "User does not exist.";
                return RedirectToPage("/Login");
            }

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
            return Page();
        }
    }
}