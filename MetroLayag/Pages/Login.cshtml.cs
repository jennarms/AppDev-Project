using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using MetroLayag.Data;
using System.Linq;
using System;

namespace MetroLayag.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Please enter both username and password.";
                return Page();
            }

            // Log typed username
            Console.WriteLine($"Typed Username: {Username}");

            // Attempt to find user
            var user = _context.Users
                .FirstOrDefault(u => u.Username.ToLower() == Username.ToLower());

            if (user == null)
            {
                ErrorMessage = "Username does not exist.";
                return Page();
            }

            // Check password match
            bool isMatch = BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash);
            Console.WriteLine($"Password match: {isMatch}");

            if (!isMatch)
            {
                ErrorMessage = "Incorrect password.";
                return Page();
            }

            // Save to session
            HttpContext.Session.SetString("IsLoggedIn", "true");
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);
            HttpContext.Session.SetString("Station", user.Station);

            if (user.Role == "MainAdmin")
            {
                return RedirectToPage("/Index");
            }
            else if (user.Role == "StationAdmin")
            {
                return RedirectToPage("/Index");
            }
            else
            {
                ErrorMessage = "Unknown user role.";
                return Page();
            }
        }
    }
}
